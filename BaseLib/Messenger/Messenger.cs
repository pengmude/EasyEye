using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SmartLib
{
    /// <summary>
    ///     信使，用于给接发送消息传参,任何对象都可以是接收端；任何对象都可以是发送端；任何对象都可以是消息。
    /// </summary>
    public class Messenger : IMessenger
    {
        private static readonly object CreationLock = new object();
        private static IMessenger _defaultInstance;
        private readonly SynchronizationContext _context = SynchronizationContext.Current;
        private readonly object _registerLock = new object();
        private bool _isCleanupRegistered;
        private Dictionary<Type, List<WeakActionAndToken>> _recipientsOfSubclassesAction { get; set; }

        /// <summary>
        /// 单例
        /// </summary>
        public static IMessenger Default
        {
            get
            {
                if (_defaultInstance == null)
                {
                    lock (CreationLock)
                    {
                        if (_defaultInstance == null)
                        {
                            _defaultInstance = new Messenger();
                        }
                    }
                }

                return _defaultInstance;
            }
        }

        /// <summary>
        /// 注册Messenger
        /// </summary>
        /// <typeparam name="TMessage">传递类型</typeparam>
        /// <param name="recipient">接收对象</param>
        /// <param name="action">当接收到消息时执行这个action</param>
        public virtual void Register<TMessage>(object recipient, Action<TMessage> action)
        {
            Register(recipient, null, action);
        }

        /// <summary>
        /// 注册Messenger
        /// </summary>
        /// <typeparam name="TMessage">传递类型</typeparam>
        /// <param name="recipient">接收对象</param>
        /// <param name="token">传递口令</param>
        /// <param name="action">当接收到消息时执行的action</param>
        public virtual void Register<TMessage>(object recipient, object token, Action<TMessage> action)
        {
            lock (_registerLock)
            {
                var messageType = typeof(TMessage);

                Dictionary<Type, List<WeakActionAndToken>> recipients;

                if (_recipientsOfSubclassesAction == null)
                {
                    _recipientsOfSubclassesAction = new Dictionary<Type, List<WeakActionAndToken>>();
                }

                recipients = _recipientsOfSubclassesAction;

                lock (recipients)
                {
                    List<WeakActionAndToken> list;

                    if (!recipients.ContainsKey(messageType))
                    {
                        list = new List<WeakActionAndToken>();
                        recipients.Add(messageType, list);
                    }
                    else
                    {
                        list = recipients[messageType];
                    }

                    var weakAction = new WeakAction<TMessage>(recipient, action);

                    var item = new WeakActionAndToken
                    {
                        Action = weakAction,
                        Token = token
                    };

                    list.Add(item);
                }
            }

            RequestCleanup();
        }

        /// <summary>
        ///注销Messenger
        /// </summary>
        /// <param name="recipient">接收对象</param>
        public virtual void UnRegister(object recipient)
        {
            if (recipient == null || _recipientsOfSubclassesAction == null || _recipientsOfSubclassesAction.Count == 0)
            {
                return;
            }

            lock (_recipientsOfSubclassesAction)
            {
                foreach (var messageType in _recipientsOfSubclassesAction.Keys)
                {
                    foreach (var item in _recipientsOfSubclassesAction[messageType])
                    {
                        var weakAction = (IExecuteWithObject)item.Action;

                        if (weakAction != null && recipient == weakAction.Target)
                        {
                            weakAction.MarkForDeletion();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 注销Messenger
        /// </summary>
        /// <typeparam name="TMessage">传递类型</typeparam>
        /// <param name="recipient">接收对象</param>
        public virtual void UnRegister<TMessage>(object recipient)
        {
            UnRegister<TMessage>(recipient, null, null);
        }

        /// <summary>
        /// 从列表中注销Messenger
        /// </summary>
        /// <typeparam name="TMessage">传递类型</typeparam>
        /// <param name="recipient">接收对象</param>
        /// <param name="token">传递口令</param>
        /// <param name="action">当接收到消息时执行的action</param>
        /// <param name="lists">Messenger列表</param>
        private static void UnregisterFromLists<TMessage>(
            object recipient,
            object token,
            Action<TMessage> action,
            Dictionary<Type, List<WeakActionAndToken>> lists)
        {
            var messageType = typeof(TMessage);

            if (recipient == null
                || lists == null
                || lists.Count == 0
                || !lists.ContainsKey(messageType))
                return;

            lock (lists)
            {
                foreach (var item in lists[messageType])
                    if (item.Action is WeakAction<TMessage> weakActionCasted
                        && recipient == weakActionCasted.Target
                        && (action == null
#if NETFX_CORE
                            || action.GetMethodInfo().Name == weakActionCasted.MethodName)
#else
                            || action.Method.Name == weakActionCasted.MethodName)
#endif
                        && (token == null
                            || token.Equals(item.Token)))
                        item.Action.MarkForDeletion();
            }
        }

        /// <summary>
        /// 注销Messenger
        /// </summary>
        /// <typeparam name="TMessage">传递类型</typeparam>
        /// <param name="recipient">接收对象</param>
        /// <param name="action">传递口令</param>
        public virtual void UnRegister<TMessage>(object recipient, Action<TMessage> action)
        {
            UnRegister(recipient, null, action);
        }

        /// <summary>
        /// 注销Messenger
        /// </summary>
        /// <typeparam name="TMessage">传递类型</typeparam>
        /// <param name="recipient">接收对象</param>
        /// <param name="token">传递口令</param>
        /// <param name="action">当接收到消息时执行的action</param>
        public virtual void UnRegister<TMessage>(object recipient, object token, Action<TMessage> action)
        {
            UnregisterFromLists(recipient, token, action, _recipientsOfSubclassesAction);
            RequestCleanup();
        }

        /// <summary>
        /// 注销Messenger
        /// </summary>
        /// <typeparam name="TMessage">传递类型</typeparam>
        /// <param name="recipient">接收对象</param>
        /// <param name="token">传递口令</param>
        public virtual void UnRegister<TMessage>(object recipient, object token)
        {
            UnRegister<TMessage>(recipient, token, null);
        }

        /// <summary>
        /// 将Messenger加入列表
        /// </summary>
        /// <typeparam name="TMessage">传递类型</typeparam>
        /// <param name="message">传递内容</param>
        /// <param name="weakActionsAndTokens">弱连接和口令</param>
        /// <param name="token">传递口令</param>
        /// <param name="messageTargetType">Messenger模板类型</param>
        private static void SendToList<TMessage>(TMessage message, IEnumerable<WeakActionAndToken> weakActionsAndTokens, object token, Type messageTargetType = null)
        {
            if (weakActionsAndTokens != null)
            {
                // Clone to protect from people registering in a "receive message" method
                // Correction Messaging BL0004.007
                var list = weakActionsAndTokens.ToList();
                var listClone = list.Take(list.Count())
                    .ToList();

                foreach (var item in listClone)
                {
                    var executeAction = item.Action as IExecuteWithObject;

                    if (executeAction != null && item.Action.IsAlive && item.Action.Target != null && (messageTargetType == null || item.Action.Target.GetType() == messageTargetType || messageTargetType.IsAssignableFrom(item.Action.Target.GetType())) && (item.Token == null && token == null || item.Token != null && item.Token.Equals(token)))
                    {
                        executeAction.ExecuteWithObject(message);
                    }
                }
            }
        }

        /// <summary>
        /// 向已注册的收件人发送邮件。该邮件将仅到达使用其中一个Register方法为此邮件类型注册的收件人以及targetType的收件人。
        /// </summary>
        /// <typeparam name="TMessage">将发送的消息类型。</typeparam>
        /// <param name="message">发送的消息</param>
        /// <param name="token">消息通道的令牌。 如果接收者使用令牌注册，
        /// 并且发送者使用相同的令牌发送消息，则该消息将被传递给接收者。
        /// 注册时未使用令牌（或使用不同令牌）的其他收件人将不会收到该消息。
        /// 同样，没有任何令牌或使用不同令牌发送的消息将不会传递给该收件人。</param>
        public void Send<TMessage>(TMessage message, object token = null)
        {
            var messageType = typeof(TMessage);

            if (_recipientsOfSubclassesAction != null)
            {
                // Clone to protect from people registering in a "receive message" method
                // Correction Messaging BL0008.002
                var listClone = _recipientsOfSubclassesAction.Keys.Take(_recipientsOfSubclassesAction.Count())
                    .ToList();

                foreach (var type in listClone)
                {
                    List<WeakActionAndToken> list = null;

                    if (messageType == type || messageType.IsSubclassOf(type) || type.IsAssignableFrom(messageType))

                    {
                        lock (_recipientsOfSubclassesAction)
                        {
                            list = _recipientsOfSubclassesAction[type]
                                .Take(_recipientsOfSubclassesAction[type]
                                    .Count())
                                .ToList();
                        }
                    }

                    SendToList(message, list, token);
                }
            }

            RequestCleanup();
        }

        /// <summary>
        ///     扫描收件人列表中的无效实例并将其删除
        /// </summary>
        public void Cleanup()
        {
            if (_recipientsOfSubclassesAction == null)
            {
                return;
            }

            lock (_recipientsOfSubclassesAction)
            {
                var listsToRemove = new List<Type>();
                foreach (var list in _recipientsOfSubclassesAction)
                {
                    var recipientsToRemove = list.Value.Where(item => item.Action == null || !item.Action.IsAlive)
                        .ToList();

                    foreach (var recipient in recipientsToRemove)
                    {
                        list.Value.Remove(recipient);
                    }

                    if (list.Value.Count == 0)
                    {
                        listsToRemove.Add(list.Key);
                    }
                }

                foreach (var key in listsToRemove)
                {
                    _recipientsOfSubclassesAction.Remove(key);
                }
            }

            _isCleanupRegistered = false;
        }

        /// <summary>
        /// 通知Messenger应扫描和清理收件人列表。
        /// 由于收件人存储为 WeakReference，即使 Messenger 将收件人保留在列表中，收件人也可能被垃圾收集。
        ///在清理操作期间，所有“死”收件人都将从列表中删除。 由于此操作可能需要一些时间，因此仅在应用程序空闲时执行。
        /// 出于这个原因，Messenger 类的用户应该使用 RequestCleanup 而不是强制使用 Cleanup
        /// </summary>
        public void RequestCleanup()
        {
            if (!_isCleanupRegistered)
            {
                Action cleanupAction = Cleanup;

                if (_context != null)
                {
                    _context.Post(_ => cleanupAction(), null);
                }
                else
                {
                    cleanupAction(); // Run inline w/o a context
                }

                _isCleanupRegistered = true;
            }
        }

        private struct WeakActionAndToken
        {
            public WeakAction Action;

            public object Token;
        }
    }
}
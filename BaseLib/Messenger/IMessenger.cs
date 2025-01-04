using System;

namespace SmartLib
{
    /// <summary>
    /// Messenger接口
    /// </summary>
    public interface IMessenger
    {
        /// <summary>
        /// 扫描收件人列表中的无效实例并将其删除
        /// </summary>
        void Cleanup();
        /// <summary>
        /// 注册Messenger
        /// </summary>
        /// <typeparam name="TMessage">传递类型</typeparam>
        /// <param name="recipient">接收对象</param>
        /// <param name="action">传递口令</param>
        void Register<TMessage>(object recipient, Action<TMessage> action);
        /// <summary>
        /// 注册Messenger
        /// </summary>
        /// <typeparam name="TMessage">传递类型</typeparam>
        /// <param name="recipient">接收对象</param>
        /// <param name="token">传递口令</param>
        /// <param name="action">当接收到消息时执行这个action</param>
        void Register<TMessage>(object recipient, object token, Action<TMessage> action);
        /// <summary>
        /// 通知Messenger应扫描和清理收件人列表。
        /// 由于收件人存储为 WeakReference，即使 Messenger 将收件人保留在列表中，收件人也可能被垃圾收集。
        ///在清理操作期间，所有“死”收件人都将从列表中删除。 由于此操作可能需要一些时间，因此仅在应用程序空闲时执行。
        /// 出于这个原因，Messenger 类的用户应该使用 RequestCleanup 而不是强制使用 Cleanup
        /// </summary>
        void RequestCleanup();
        /// <summary>
        /// Messenger发送消息
        /// </summary>
        /// <typeparam name="TMessage">传递类型</typeparam>
        /// <param name="message">传递内容</param>
        /// <param name="token">传递口令</param>
        void Send<TMessage>(TMessage message, object token = null);
        /// <summary>
        /// 注销Messenger
        /// </summary>
        /// <param name="recipient">接收对象</param>
        void UnRegister(object recipient);

        /// <summary>
        /// 注销Messenger
        /// </summary>
        /// <typeparam name="TMessage">传递类型</typeparam>
        /// <param name="recipient">接收对象</param>
        void UnRegister<TMessage>(object recipient);

        /// <summary>
        /// 注销Messenger
        /// </summary>
        /// <typeparam name="TMessage">传递类型</typeparam>
        /// <param name="recipient">接收对象</param>
        /// <param name="token">传递口令</param>
        void UnRegister<TMessage>(object recipient, object token);

        /// <summary>
        /// 注销Messenger
        /// </summary>
        /// <typeparam name="TMessage">传递类型</typeparam>
        /// <param name="recipient">接收对象</param>
        /// <param name="action">当接收到消息时执行这个action</param>
        void UnRegister<TMessage>(object recipient, Action<TMessage> action);

        /// <summary>
        /// 注销Messenger
        /// </summary>
        /// <typeparam name="TMessage">传递类型</typeparam>
        /// <param name="recipient">接收对象</param>
        /// <param name="token">传递口令</param>
        /// <param name="action">当接收到消息时执行这个action</param>
        void UnRegister<TMessage>(object recipient, object token, Action<TMessage> action);
    }
}
using System;
using System.Reflection;

namespace SmartLib
{
    /// <summary>
    /// 封装一个弱引用Action，让拥有者可以随时用垃圾回收器收集
    /// 避免内存泄漏
    /// </summary>
    public class WeakAction
    {
        private Action _staticAction;

        /// <summary>
        /// 弱引用构造函数
        /// </summary>
        public WeakAction()
        {
        }

        /// <summary>
        /// 弱引用构造函数
        /// </summary>
        /// <param name="action">哦当作</param>
        /// <param name="keepTargetAlive">是否持续保持</param>
        public WeakAction(Action action, bool keepTargetAlive = false) : this(action == null ? null : action.Target, action, keepTargetAlive)
        {
        }

        /// <summary>
        /// 弱引用构造函数
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="action">动作</param>
        /// <param name="keepTargetAlive">是否持续保持</param>
        public WeakAction(object target, Action action, bool keepTargetAlive = false)
        {
            if (action.GetMethodInfo()
                .IsStatic)
            {
                _staticAction = action;

                if (target != null)
                {
                    // Keep a reference to the target to control the
                    // WeakAction's lifetime.
                    Reference = new WeakReference(target);
                    return;
                }

                Method = action.GetMethodInfo();
                ActionReference = new WeakReference(action.Target);
                LiveReference = keepTargetAlive ? action.Target : null;
                Reference = new WeakReference(target);
            }
        }

        /// <summary>
        /// 提供方法的元数据信息访问，通过构造方法访问
        /// </summary>
        protected MethodInfo Method { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public virtual string MethodName
        {
            get
            {
                if (_staticAction != null)
                {
                    return _staticAction.GetMethodInfo()
                        .Name;
                }

                return Method.Name;
            }
        }

        /// <summary>
        /// 给动态的弱方法的目标设置一个弱引用
        /// </summary>
        protected WeakReference ActionReference { get; set; }

        /// <summary>
        /// 给静态的弱方法的目标设置一个弱引用
        /// </summary>
        protected WeakReference Reference { get; set; }

        /// <summary>
        /// 设置一个强引用，当构造方法的keepTargetAlive参数是True的时候生效
        /// </summary>
        protected object LiveReference { get; set; }

        /// <summary>
        /// 返回一个值指明是否方法的拥有者仍活着或者已经被垃圾回收器回收了
        /// </summary>
        public virtual bool IsAlive
        {
            get
            {
                if (_staticAction == null && Reference == null && LiveReference == null)
                {
                    return false;
                }

                if (_staticAction != null)
                {
                    if (Reference != null)
                    {
                        return Reference.IsAlive;
                    }

                    return true;
                }

                // Non static action

                if (LiveReference != null)
                {
                    return true;
                }

                if (Reference != null)
                {
                    return Reference.IsAlive;
                }

                return false;
            }
        }

        /// <summary>
        /// 返回一个值指明方法的拥有者
        /// </summary>
        public object Target
        {
            get
            {
                if (Reference == null)
                {
                    return null;
                }

                return Reference.Target;
            }
        }

        /// <summary>
        /// 返回弱引用的拥有者
        /// </summary>
        protected object ActionTarget
        {
            get
            {
                if (LiveReference != null)
                {
                    return LiveReference;
                }

                if (ActionReference == null)
                {
                    return null;
                }

                return ActionReference.Target;
            }
        }

        /// <summary>
        /// 如果拥有者还生还，则执行
        /// </summary>
        public void Execute()
        {
            if (_staticAction != null)
            {
                _staticAction();
                return;
            }

            var actionTarget = ActionTarget;

            if (IsAlive)
            {
                if (Method != null && (LiveReference != null || ActionReference != null) && actionTarget != null)
                {
                    Method.Invoke(actionTarget, null);

                    // ReSharper disable RedundantJumpStatement
                    return;
                    // ReSharper restore RedundantJumpStatement
                }
            }
        }

        /// <summary>
        /// 将此实例的引用设置为null。
        /// </summary>
        public void MarkForDeletion()
        {
            Reference = null;
            ActionReference = null;
            LiveReference = null;
            Method = null;
            _staticAction = null;
        }
    }

    /// <summary>
    /// 弱引用类
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class WeakAction<T> : WeakAction, IExecuteWithObject
    {
        private Action<T> _staticAction;

        /// <summary>
        /// 初始化 WeakAction 类的新实例。初始化 WeakAction 类的新实例。
        /// </summary>
        public WeakAction(Action<T> action, bool keepTargetAlive = false) : this(action == null ? null : action.Target, action, keepTargetAlive)
        {

        }

        /// <summary>
        /// 初始化 WeakAction 类的新实例
        /// </summary>
        /// <param name="target">action动作的拥有者</param>
        /// <param name="action">执行的action</param>
        /// <param name="keepTargetAlive">如果为true,则Action的目标将保留为强引用,这可能会导致内存泄漏;
        /// 仅当操作使用闭包时,才将此参数设置为true; 可以参考http://galasoft.ch/s/mvvmweakaction</param>
        public WeakAction(object target, Action<T> action, bool keepTargetAlive = false)
        {
            if (action.Method.IsStatic)

            {
                _staticAction = action;

                if (target != null)
                {
                    // Keep a reference to the target to control the
                    // WeakAction's lifetime.
                    Reference = new WeakReference(target);
                }

                return;
            }

            Method = action.Method;

            ActionReference = new WeakReference(action.Target);

            LiveReference = keepTargetAlive ? action.Target : null;
            Reference = new WeakReference(target);
        }

        
        /// <summary>
        /// 获取弱引用方法名称
        /// </summary>
        public override string MethodName
        {
            get
            {
                if (_staticAction != null)
                {
                    return _staticAction.Method.Name;
                }

                return Method.Name;
            }
        }
        
        /// <summary>
        /// 查看当前引用的所有者是否仍然存活
        /// </summary>
        public override bool IsAlive
        {
            get
            {
                if (_staticAction == null && Reference == null)
                {
                    return false;
                }

                if (_staticAction != null)
                {
                    if (Reference != null)
                    {
                        return Reference.IsAlive;
                    }

                    return true;
                }

                return Reference.IsAlive;
            }
        }

        /// <summary>
        /// 使用一个object类型参数执行action
        /// </summary>
        /// <param name="parameter">参数</param>
        public void ExecuteWithObject(object parameter)
        {
            var parameterCasted = (T)parameter;
            Execute(parameterCasted);
        }

        /// <summary>
        /// 标记弱引被删除
        /// </summary>
        public new void MarkForDeletion()
        {
            _staticAction = null;
            base.MarkForDeletion();
        }

        /// <summary>
        /// 执行弱引用
        /// </summary>
        public new void Execute()
        {
            Execute(default);
        }

        /// <summary>
        /// 执行弱引用
        /// </summary>
        /// <param name="parameter">参数</param>
        public void Execute(T parameter)
        {
            if (_staticAction != null)
            {
                _staticAction(parameter);
                return;
            }

            var actionTarget = ActionTarget;

            if (IsAlive)
            {
                if (Method != null && (LiveReference != null || ActionReference != null) && actionTarget != null)
                {
                    Method.Invoke(actionTarget, new object[]
                    {
                        parameter
                    });
                }
            }
        }
    }

    /// <summary>
    /// 这个接口是为WeakAction类设计的，如果您存储了多个WeakAction{T}实例，但事先不知道T代表什么类型，那么这个接口就非常有用。
    /// </summary>
    public interface IExecuteWithObject
    {
        /// <summary>
        /// 引用的参数
        /// </summary>
        object Target { get; }

        /// <summary>
        /// 执行引用
        /// </summary>
        /// <param name="parameter">使用的参数</param>
        void ExecuteWithObject(object parameter);

        /// <summary>
        /// 删除所有引用
        /// </summary>
        void MarkForDeletion();
    }
}
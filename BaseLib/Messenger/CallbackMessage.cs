using System;

namespace SmartLib
{
    /// <summary>
    /// 回调消息类，发送者可调用Execute方法接收收件人发送的消息。实现双工通信
    /// </summary>
    /// <typeparam name="TCallbackParameter"></typeparam>
    public class CallbackMessage<TCallbackParameter>
    {
        private readonly Delegate _callback;
        /// <summary>
        /// 回调消息
        /// </summary>
        /// <param name="callback">回调执行动作</param>
        public CallbackMessage(Action<TCallbackParameter> callback)
        {
            _callback = callback;
        }


        /// <summary>
        ///     使用任意数量的参数执行随消息提供的回调。
        /// </summary>
        /// <param name="arguments">将传递给回调方法的一些参数。</param>
        /// <returns>回调方法返回的对象。</returns>
        public virtual object Execute(params string[][] arguments)
        {
            if (_callback == null)
            {
                throw new ArgumentNullException("callback", "Callback may not be null");
            }

            return _callback.DynamicInvoke(arguments);
        }

        /// <summary>
        /// 信息
        /// </summary>
        public object Msg { get; set; }
    }



}
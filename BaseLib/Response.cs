namespace SmartLib
{
    /// <summary>
    /// 带参数的Response
    /// </summary>
    /// <typeparam name="TResult">参数类型</typeparam>
    public class Response<TResult>
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool IsSuccessful { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErroCode { get; set; }
        /// <summary>
        /// 返回结果数据
        /// </summary>
        public TResult Data { get; set; }
        /// <summary>
        /// 是否弹出错误信息
        /// </summary>
        public bool IsShowErrMsg { get; set; } = true;

        /// <summary>
        ///     成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        public static Response<TResult> Ok(TResult data)
        {
            return new Response<TResult>
            {
                Data = data,
                Msg = null,
                IsSuccessful = true,
                ErroCode = "0"
            };
        }

        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="str">错误信息</param>
        /// <param name="data">数据</param>
        /// <param name="code">错误代码</param>
        /// <returns>返回结果</returns>
        public static Response<TResult> Fail(string str, TResult data = default, string code = "0")
        {
            return new Response<TResult>
            {
                Data = data,
                Msg = str,
                IsSuccessful = false,
                ErroCode = code
            };
        }

        /// <summary>
        /// 返回错误信息
        /// </summary>
        /// <param name="res"></param>
        public static implicit operator string(Response<TResult> res)
        {
            return res.Msg;
        }

        /// <summary>
        /// 返回OKNG结果
        /// </summary>
        /// <param name="res"></param>
        public static implicit operator bool(Response<TResult> res)
        {
            return res.IsSuccessful;
        }

        /// <summary>
        /// 返回错误信息 转换为string类型
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Msg;
        }
    }

    /// <summary>
    /// Response类
    /// </summary>
    public class Response
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool IsSuccessful { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; } = "";
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErroCode { get; set; } = "0";
        /// <summary>
        /// 是否弹出错误信息
        /// </summary>
        public bool IsShowErrMsg { get; set; } = true;

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg">信息内容</param>
        /// <returns>返回结果</returns>
        public static Response Ok(string msg = "")
        {
            return new Response
            {
                Msg = msg,
                IsSuccessful = true
            };
        }

        /// <summary>
        ///     失败
        /// </summary>
        /// <param name="str">错误信息</param>
        /// <param name="code">错误代码</param>
        /// <returns></returns>
        public static Response Fail(string str, string code = "0")
        {
            return new Response
            {
                Msg = str,
                IsSuccessful = false,
                ErroCode = code
            };
        }

        /// <summary>
        /// 返回错误信息
        /// </summary>
        /// <param name="res"></param>
        public static implicit operator string(Response res)
        {
            return res.Msg;
        }

        /// <summary>
        /// 返回OKNG结果
        /// </summary>
        /// <param name="res"></param>
        public static implicit operator bool(Response res) 
        {
            return res.IsSuccessful;//返回目标实例的数据。
        }
        /// <summary>
        /// 返回错误信息 转换为string类型
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Msg;
        }

    }
}
using System;
using System.ComponentModel;

namespace BaseData
{
    /// <summary>
    /// 用户权限等级枚举
    /// </summary>
    [Serializable]
    public enum UserLevel
    {
        /// <summary>
        /// 操作员
        /// </summary>
        [Description("操作员")] Operator = 0,
        /// <summary>
        /// 技术员
        /// </summary>
        [Description("技术员")] Technician = 1,
        /// <summary>
        /// 工程师
        /// </summary>
        [Description("工程师")] Engineer = 2,
        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")] Admin = 3,
        /// <summary>
        /// 开发员
        /// </summary>
        [Description("开发员")] SuperAdmin = 4,
        /// <summary>
        /// 超级开发员
        /// </summary>
        [Description("超级开发员")] SSuperAdmin = 5
    }

    /// <summary>
    /// 系统用户类
    /// </summary>
    [Serializable]
    public class UserObj
    {
        private string uName = "操作员";
        private string uPwd = "123";
        private UserLevel uLevel = UserLevel.Operator;
        private string uLvlName = "操作员";
        /// <summary>
        /// 用户名
        /// </summary>
        public string UName { get => uName; set => uName = value; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UPwd { get => uPwd; set => uPwd = value; }
        /// <summary>
        /// 用户权限等级
        /// </summary>
        public UserLevel ULevel { get => uLevel; set => uLevel = value; }
        /// <summary>
        /// 用户权限名称
        /// </summary>
        public string ULvlName { get => uLvlName; set => uLvlName = value; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserObj() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">用户密码</param>
        /// <param name="level">用户权限等级</param>
        /// <param name="lvlname">用户权限名称</param>
        public UserObj(string name, string pwd, UserLevel level, string lvlname)
        {
            uName = name;
            uPwd = pwd;
            uLevel = level;
            uLvlName = lvlname;
        }
    }
}

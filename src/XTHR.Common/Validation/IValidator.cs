namespace XTHR.Common.Validation
{
    /// <summary>
    /// 定义验证器的接口。
    /// </summary>
    /// <typeparam name="T">要验证的对象类型。</typeparam>
    public interface IValidator<in T>
    {
        /// <summary>
        /// 验证指定的对象实例。
        /// </summary>
        /// <param name="instance">要验证的对象实例。</param>
        /// <returns>验证结果。</returns>
        ValidationResult Validate(T instance);
    }
}
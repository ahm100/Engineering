namespace Engineering.Api.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class ResponseSchemaAttribute<T> : ProducesResponseTypeAttribute
    {
        public ResponseSchemaAttribute()
            : base(typeof(Result<T>), StatusCodes.Status200OK)
        {
        }
    }
}
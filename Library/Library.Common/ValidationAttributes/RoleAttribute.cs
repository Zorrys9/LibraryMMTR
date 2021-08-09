using System.ComponentModel.DataAnnotations;

namespace Library.Common.ValidationAttributes
{
    /// <summary>
    /// Аттрибут проверки поля роли пользователя на валидность
    /// </summary>
    public class RoleAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var role = value.ToString();
                if(role.ToUpper() == "admin" && role.ToUpper() == "user")
                {
                    return true;
                }
            }
            return false;
        }
    }
}

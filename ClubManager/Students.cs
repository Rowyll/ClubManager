//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClubManager
{
    using System;
    using System.Collections.Generic;
    
    public partial class Students
    {
        public int IdStudent { get; set; }
        public string FullName { get; set; }
        public int GroupId { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    
        public virtual Groups Groups { get; set; }
    }
}

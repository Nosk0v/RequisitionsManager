//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RequisitionsManager.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Req
    {
        public int ReqID { get; set; }
        public string ReqName { get; set; }
        public string Description { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime Deadline { get; set; }
        public int Status { get; set; }
        public int PerformerReq { get; set; }
    
        public virtual Performer Performer { get; set; }
        public virtual StatusType StatusType { get; set; }
    }
}

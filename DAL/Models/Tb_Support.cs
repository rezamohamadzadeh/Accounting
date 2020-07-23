using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Tb_Support
    {
        public Tb_Support()
        {
            SendDateTime = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(SupportType))]        
        public int TypeId { get; set; }

        public string Message { get; set; }
        public DateTime SendDateTime { get; set; }

        //[ForeignKey(nameof(SenderUser))]
        public string SenderUserId { get; set; }

        public string AnswerMessage { get; set; }        
        public DateTime? AnswerDateTime { get; set; }

        //[ForeignKey(nameof(AnswerUser))]
        public string AnswerUserId { get; set; }

        public SupportPosition SupportPosition { get; set; }

        public string File { get; set; }

        public string Email { get; set; }

        //public ApplicationUser SenderUser { get; set; }
        //public ApplicationUser AnswerUser { get; set; }
        public Tb_SupportType SupportType { get; set; }
    }

    public enum SupportPosition
    {
        Pending = 1,
        Delivered = 2,
        Anwsered = 3
    }
}

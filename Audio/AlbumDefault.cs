using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio
{
    class AlbumDefault
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AlbumId { get; set; }
    }
}

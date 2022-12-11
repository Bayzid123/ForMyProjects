using System;
using System.Collections.Generic;

namespace ApiForMyProjects.Models
{
    public partial class TblTrack
    {
        public long IntTrackId { get; set; }
        public string StrTrackName { get; set; }
        public long? IntAlbumId { get; set; }
        public string StrComposer { get; set; }
        public bool IsActive { get; set; }
        public DateTime DteCreatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AsssetManagement.Models
{
    public class Asset
    {
        public Guid Id { get; set; }
        public string AssetTag { get; set; }
        public AssetType Type { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Project { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AssetStatus Status { get; set; }
        public bool IsRequested { get; set; }
        public bool IsSearched { get; set; }
    }
    public enum AssetStatus
    {
        InStore = 1,
        Requested = 2,
        Assigned = 3
    }
    public enum AssetType
    {
        Mobile = 1,
        Tablet = 2,
        Laptop = 3
    }
}

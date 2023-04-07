using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilizationsRoad.Scripts.Common;

public partial class Configuration : Node
{
    public partial struct Programs
    {
        // TODO: 验证非 [Export] 的元素 Godot 是否能访问到
        [Export]
        public static bool IsDebug { get; set; }
        
    }
}

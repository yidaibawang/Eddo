namespace Eddo.Permissions.Authorization
{
    public class PermissionGrantInfo
    {   
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 是否授权
        /// </summary>
        public bool IsGranted { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="PermissionGrantInfo"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isGranted"></param>
        public PermissionGrantInfo(string name, bool isGranted)
        {
            Name = name;
            IsGranted = isGranted;
        }
    }
}


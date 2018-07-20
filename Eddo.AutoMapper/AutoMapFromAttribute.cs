using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Eddo.Collections.Extensions;

namespace Eddo.AutoMapper
{
    public class AutoMapFromAttribute : AutoMapAttributeBase
    {
        public MemberList MemberList { get; set; } = MemberList.Destination;

        public AutoMapFromAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {

        }

        public AutoMapFromAttribute(MemberList memberList, params Type[] targetTypes)
            : this(targetTypes)
        {
            MemberList = memberList;
        }

        public override void CreateMap(IMapperConfigurationExpression configuration, Type type)
        {
            if (TargetTypes.IsNullOrEmpty())
            {
                return;
            }

            foreach (var targetType in TargetTypes)
            {
                configuration.CreateMap(targetType, type, MemberList);
            }
        }
    }
}

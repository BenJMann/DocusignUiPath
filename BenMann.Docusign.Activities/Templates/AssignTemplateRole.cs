using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Templates
{

    [DisplayName("Assign Template Role")]
    public sealed class AssignTemplateRole : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<object> Template { get; set; }
        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Role Name")]
        public InArgument<string> RoleName { get; set; }
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Email { get; set; }
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Name { get; set; }


        protected override void Execute(CodeActivityContext context)
        {
            Template template = (Template)Template.Get(context);
            string roleName = RoleName.Get(context);
            string email = Email.Get(context);
            string name = Name.Get(context);

            template.AddRole(new Role(roleName, name, email));
        }
    }
}

﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Moodle_1._0.Startup))]
namespace Moodle_1._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using ChessUI.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessUI.FactoryAndBuild.Builders
{
    class Director
    {
        private readonly 
        private readonly IBuilder _builder;

        public Director(IBuilder builder)
        {
            _builder = builder;
        }

        public Presenter Build()
        {
            _builder.

        }
    }
}

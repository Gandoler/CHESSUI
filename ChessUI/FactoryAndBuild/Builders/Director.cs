using ChessUI.Code;
using ChessUI.FactoryAndBuild.Factory;
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
        private readonly IPresenterComponentsFactory _factory;
        private readonly IBuilder _builder;

        public Director(IBuilder builder,IPresenterComponentsFactory factory)
        {
            _builder = builder;
            _factory = factory;
        }

        public Presenter Build()
        {
            _builder.WithChessWindow(_factory.CreateView())
                .WithGameState(_factory

        }
    }
}

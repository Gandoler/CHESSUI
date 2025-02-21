using ChessUI.Code;
using ChessUI.FactoryAndBuild.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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

        public void Build()
        {
            
             _builder.WithChessWindow(_factory.CreateView())
                .WithModelka(_factory.CreateModel())
                .WithPromotionMenu(_factory.CreatePromotionMenu())
                .WithGameOverMenu(_factory.CreateGameOverMenu())
                .WithPauseMenu(_factory.CreatePauseMenu())
                .WithBrush(new SolidColorBrush(Color.FromArgb(150, 125, 255, 125)))
                .Build();


        }
    }
}

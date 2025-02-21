using ChessLogic.GameState;
using ChessUI.Code;
using ChessUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChessUI.FactoryAndBuild.Builders
{
    interface IBuilder
    {
        

        public Presenter Build();
    }
}

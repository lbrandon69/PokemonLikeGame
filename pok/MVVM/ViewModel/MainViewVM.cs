using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PokemonLikeProject.MVVM.View;

namespace PokemonLikeProject.MVVM.ViewModel
{
    public class MainViewVM : BaseVM
    {
        //Called from view (With data binding)
        public ICommand RequestChangeViewCommand { get; set; }
        public MainViewVM()
        {
            //Configure command to callback "HandleRequestChangeViewCommand"
            //when command is called
            RequestChangeViewCommand = new RelayCommand(HandleRequestChangeViewCommand);
        }

        public void HandleRequestChangeViewCommand()
        {
          
        }

        //Override from BaseVM
        
    }
}
using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using ConvMVVM2.WPF.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Test.WPF.ViewModel
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        #region Private Property
        private readonly IRegionManager regionManager;
        private readonly IDialogService dialogService;
        private readonly IUndoService undoService;
        #endregion

        #region Constructor
        public MainWindowViewModel(IRegionManager regionManager,
                                   IDialogService dialogService,
                                   IUndoService undoService) { 
        
            this.regionManager = regionManager;
            this.dialogService = dialogService;
            this.undoService = undoService;



            this.IsUndoEnabled = true;
        }
        #endregion

        #region Public Property

        [Property]
        [PropertyChangedFor("Test2")]
        [PropertyChangedFor("Test3")]
        private string _Test = "test";

        [Property]
        private string _Test2 = "test";


        [Property]
        public string _Test3 = "";


        [Property]
        private IList _ItemsCollection = null;


        [Property]
        private ObservableCollection<string> _TestCollection = new ObservableCollection<string>();
        #endregion


        #region Command
        [RelayCommand]
        private void AView()
        {
            try
            {



            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }


        [RelayCommand]
        private void BView()
        {
            try
            {


                using (this.undoService.BeginGroup())
                {
                    this.Test = "A";
                    this.Test2 = "B";
                    this.Test3 = "C";
                }


                
                

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        [RelayCommand]
        private void TestTT()
        {
            try
            {
                this.undoService.Undo();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        [RelayCommand]
        private void TestTT2()
        {
            try
            {
                this.undoService.Redo();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
        #endregion

        #region Event Handler
        partial void OnTestChanged(string oldValue, string newValue)
        {
            this.undoService.Do(PropertyUndoAction<string>.Create(this, oldValue, newValue, "Test"));
        }

        partial void OnTest2Changed(string oldValue, string newValue)
        {
            this.undoService.Do(PropertyUndoAction<string>.Create(this, oldValue, newValue, "Test2"));
        }

        partial void OnTest3Changed(string oldValue, string newValue)
        {
            this.undoService.Do(PropertyUndoAction<string>.Create(this, oldValue, newValue, "Test3"));
        }
        #endregion


        #region Public Functions
        public void SelectionChangedCommand(object args)
        {
            System.Diagnostics.Debug.WriteLine("test");

        }
        #endregion

    }
}

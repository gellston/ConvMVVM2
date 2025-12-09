using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using ConvMVVM2.WPF.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public string Test3 { get; set; } = "";


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

                this.TestCollection.AddWithUndo(this.undoService, "test");
                this.TestCollection.AddWithUndo(this.undoService, "test");
                this.TestCollection.AddWithUndo(this.undoService, "test");
                this.TestCollection.AddWithUndo(this.undoService, "test");
                

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
        #endregion

        #region Public Functions
        public void SelectionChangedCommand(object args)
        {
            System.Diagnostics.Debug.WriteLine("test");

        }
        #endregion
    }
}

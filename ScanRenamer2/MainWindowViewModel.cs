using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Data;

namespace ScanRenamer2
{
    class MainWindowViewModel : BindableBase
    {
        private const string scannedFilesLocation = @"C:\Users\Ruedi\Documents\Nicht gesichert\Scan";
        private ObservableCollection<ScannedFile> scannedFiles = null;
        private CollectionViewSource scannedFilesViewSource = null;
        public ICollectionView ScannedFiles { get; private set; }

        public DelegateCommand<ScannedFile> ScannedFileDoubleClickCommand { get; private set; }

        private const string matchedFilesLocation = @"C:\Users\Ruedi\Documents\Dokumentation\Systeme";

        private ObservableCollection<ScannedFile> matchedFiles = null;
        private CollectionViewSource matchedFilesViewSource = null;
        private ICollectionView matchedFilesView = null;
        public ICollectionView MatchedFiles
        {
            get
            {
                return matchedFilesView;
            }

            private set
            {
                matchedFilesView = value;
                RaisePropertyChanged(nameof(MatchedFiles));
            }
        }

        private DateTime datum = DateTime.Now;

        public DateTime Datum
        {
            get
            {
                return datum;
            }
            set
            {
                if (datum != value)
                {
                    datum = value;
                    RaisePropertyChanged(nameof(Datum));
                }
            }
        }

        public DelegateCommand<ScannedFile> MatchedFileDoubleClickCommand { get; private set; }

        public DelegateCommand SearchCommand { get; private set; }

        public string SearchText { get; set; }

        private bool isTabItemScannedFilesSelected = true;
        public bool IsTabItemScannedFilesSelected
        {
            get
            {
                return isTabItemScannedFilesSelected;
            }
            set
            {
                if (isTabItemScannedFilesSelected != value)
                {
                    isTabItemScannedFilesSelected = value;
                    RaisePropertyChanged(nameof(IsTabItemScannedFilesSelected));
                }
            }
        }

        private bool isTabItemNameItSelected = true;
        public bool IsTabItemNameItSelected
        {
            get
            {
                return isTabItemNameItSelected;
            }
            set
            {
                if (isTabItemNameItSelected != value)
                {
                    isTabItemNameItSelected = value;
                    RaisePropertyChanged(nameof(IsTabItemNameItSelected));
                }
            }
        }

        private bool isTabItemZusammenfassungSelected = true;
        public bool IsTabItemZusammenfassungSelected
        {
            get
            {
                return isTabItemZusammenfassungSelected;
            }
            set
            {
                if (isTabItemZusammenfassungSelected != value)
                {
                    isTabItemZusammenfassungSelected = value;
                    RaisePropertyChanged(nameof(IsTabItemZusammenfassungSelected));
                }
            }
        }

        private string sourceDirectory = null;
        public string SourceDirectory
        {
            get
            {
                return sourceDirectory;
            }
            set
            {
                if (sourceDirectory != value)
                {
                    sourceDirectory = value;
                    RaisePropertyChanged(nameof(SourceDirectory));
                }
            }
        }

        private string sourceFilename = null;
        public string SourceFilename
        {
            get
            {
                return sourceFilename;
            }
            set
            {
                if (sourceFilename != value)
                {
                    sourceFilename = value;
                    RaisePropertyChanged(nameof(SourceFilename));
                }
            }
        }

        private string destinationDirectory = null;
        public string DestinationDirectory
        {
            get
            {
                return destinationDirectory;
            }
            set
            {
                if (destinationDirectory != value)
                {
                    destinationDirectory = value;
                    RaisePropertyChanged(nameof(DestinationDirectory));
                }
            }
        }

        private string destinationFilename = null;
        public string DestinationFilename
        {
            get
            {
                return destinationFilename;
            }
            set
            {
                if (destinationFilename != value)
                {
                    destinationFilename = value;
                    RaisePropertyChanged(nameof(DestinationFilename));
                }
            }
        }


        public DelegateCommand MoveFile { get; private set; }

        public MainWindowViewModel()
        {
            scannedFiles = new ObservableCollection<ScannedFile>();
            foreach( string filename in Directory.GetFiles(scannedFilesLocation))
            {
                scannedFiles.Add(new ScannedFile(filename.Replace(scannedFilesLocation + "\\", ""), scannedFilesLocation));
            }
            scannedFilesViewSource = new CollectionViewSource()
            {
                Source = scannedFiles
            };
            ScannedFiles = scannedFilesViewSource.View;
            ScannedFileDoubleClickCommand = new DelegateCommand<ScannedFile>(ScannedFileDoubleClicked);

            SearchCommand = new DelegateCommand(Search);
            MatchedFileDoubleClickCommand = new DelegateCommand<ScannedFile>(MatchedFileDoubleClicked);

            MoveFile = new DelegateCommand(MoveFileClicked);
        }

        private ScannedFile scannedFile = null;
        private void ScannedFileDoubleClicked(ScannedFile scannedFile)
        {
            string fullSpec = scannedFilesLocation + "\\" + scannedFile.Filename;
            Process.Start(new ProcessStartInfo( fullSpec) { UseShellExecute = true });
            IsTabItemNameItSelected = true;
            Datum = DateTime.Today;
            this.scannedFile = scannedFile;
        }

        private void Search()
        {
            matchedFiles = new ObservableCollection<ScannedFile>();
            foreach(string filename in Directory.GetFiles(@"C:\Users\Ruedi\Documents\Dokumentation\Systeme", 
                string.Format( "*{0}*", SearchText), SearchOption.AllDirectories))
            {
                int pos = filename.LastIndexOf("\\");
                string path = filename.Substring(0, pos);
                string pureFilename = filename.Substring(pos + 1, filename.Length - pos - 1);
                matchedFiles.Add(new ScannedFile(pureFilename, path));
            }
            matchedFilesViewSource = new CollectionViewSource()
            {
                Source = matchedFiles
            };
            MatchedFiles = matchedFilesViewSource.View;
        }

        private ScannedFile matchedFile = null;
        private void MatchedFileDoubleClicked(ScannedFile matchedFile)
        {
            this.matchedFile = matchedFile; 
            IsTabItemZusammenfassungSelected = true;
            SourceDirectory = scannedFile.Path;
            SourceFilename = scannedFile.Filename;
            DestinationDirectory = matchedFile.Path;
            DestinationFilename = matchedFile.Filename;

            // Buchhaltung - Rule
            if ( DestinationDirectory.EndsWith( "Bezahlte Rechnungen"))
            {
                int posJahreszahl = DestinationDirectory.IndexOf( "Buchhaltung\\20") + 11;
                DestinationDirectory = string.Format("{0}\\{1}\\Offene Rechnungen",
                    DestinationDirectory.Substring(0, posJahreszahl), DateTime.Now.Year);
            }

            // Beim Filenamen ein allfälliges Datum löschen
            if( DestinationFilename.StartsWith( "20"))
            {
                DestinationFilename = DestinationFilename.Substring(13);
            }

            // Filenamen mit Datum ergänzen
            DestinationFilename = string.Format("{0} - {1}", Datum.ToString("yyyy-MM-dd"), DestinationFilename);
        }

        private void MoveFileClicked()
        {
            IsTabItemScannedFilesSelected = true;
        }
    }
}

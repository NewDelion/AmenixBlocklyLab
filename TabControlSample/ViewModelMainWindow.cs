﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TabControlSample
{
    public interface IViewModelMainWindow
    {
        bool CanMoveTabs { get; set; }
        bool ShowAddButton { get; set; }
    }

    public class ViewModelMainWindow : ViewModelExampleBase, IViewModelMainWindow
    {
        private bool _canMoveTabs;
        public bool CanMoveTabs
        {
            get { return _canMoveTabs; }
            set
            {
                if (_canMoveTabs != value)
                {
                    Set(() => CanMoveTabs, ref _canMoveTabs, value);
                }
            }
        }
        //this property is to show you can bind the visibility of the add button
        private bool _showAddButton;
        public bool ShowAddButton
        {
            get { return _showAddButton; }
            set
            {
                if (_showAddButton != value)
                {
                    Set(() => ShowAddButton, ref _showAddButton, value);
                }
            }
        }


        public ViewModelMainWindow()
        {
            //Adding items to the collection creates a tab
            this.ItemCollection.Add(CreateMyTab());

            this.SelectedTab = this.ItemCollection.FirstOrDefault();
            ICollectionView view = CollectionViewSource.GetDefaultView(this.ItemCollection) as ICollectionView;

            //This sort description is what keeps the source collection sorted, based on tab number. 
            //You can also use the sort description to manually sort the tabs, based on your own criterias.
            view.SortDescriptions.Add(new SortDescription("TabNumber", ListSortDirection.Ascending));

            this.CanMoveTabs = true;
            this.ShowAddButton = true;
        }
    }
}

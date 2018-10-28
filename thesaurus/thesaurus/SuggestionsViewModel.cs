﻿using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using Dynamo.Graph.Nodes;

namespace thesaurus
{
    public class SuggestionsViewModel : ViewModelBase
    {
        public SuggestionsModel Model { get; set; }

        private ObservableCollection<SuggestionsNodeViewModel> _nodes;
        public ObservableCollection<SuggestionsNodeViewModel> Nodes
        {
            get { return _nodes; }
            set { _nodes = value; RaisePropertyChanged(() => Nodes); }
        }

        public SuggestionsViewModel(SuggestionsModel model)
        {
            Model = model;
            this.Model.DynamoViewModel.Model.CurrentWorkspace.NodeAdded += delegate(NodeModel nodeModel)
            {
                string[] predictions = model.Predict(nodeModel.CreationName);
                // TODO: Hook up with running ML module here and provide nodeModel.CreationName as input
                // Then construct a SuggestionsNodeViewModel based on that info, the panel should update automatically
                foreach (var predictedNode in predictions)
                {
                    Nodes.Add(new SuggestionsNodeViewModel(model) { NodeName = predictedNode.Split('@')[0] });
                }
            };

            //Nodes = new ObservableCollection<SuggestionsNodeViewModel>
            //{
            //    new SuggestionsNodeViewModel(model) {NodeName = "Point.ByCoordinates"},
            //    new SuggestionsNodeViewModel(model) {NodeName = "Line.ByStartPointEndPoint"}
            //};
        }
    }
}

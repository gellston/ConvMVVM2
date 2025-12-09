using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ConvMVVM2.WPF.Extensions
{
    public static class ObservableCollectionUndoExtensions
    {

        #region Static Functions

        public static void AddWithUndo<T>(this ObservableCollection<T> collection, IUndoService undoService, T item)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (undoService == null) throw new ArgumentNullException(nameof(undoService));

            int index = collection.Count;
            collection.Insert(index, item);

            undoService.Do(new DelegateUndoAction(
                undo: () => collection.RemoveAt(index),
                redo: () => collection.Insert(index, item)
            ));
        }

        public static bool RemoveWithUndo<T>(this ObservableCollection<T> collection, IUndoService undoService, T item)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (undoService == null) throw new ArgumentNullException(nameof(undoService));

            int index = collection.IndexOf(item);
            if (index < 0)
                return false;


            collection.RemoveAt(index);


            undoService.Do(new DelegateUndoAction(
                undo: () => collection.Insert(index, item),
                redo: () => collection.RemoveAt(index)
            ));

            return true;
        }


        public static void InsertWithUndo<T>(this ObservableCollection<T> collection, IUndoService undoService, int index, T item)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (undoService == null) throw new ArgumentNullException(nameof(undoService));

            collection.Insert(index, item);

            undoService.Do(new DelegateUndoAction(
                undo: () => collection.RemoveAt(index),
                redo: () => collection.Insert(index, item)
            ));
        }


        public static void ReplaceWithUndo<T>(this ObservableCollection<T> collection, IUndoService undoService, int index, T newItem)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (undoService == null) throw new ArgumentNullException(nameof(undoService));

            var oldItem = collection[index];
            collection[index] = newItem;

            undoService.Do(new DelegateUndoAction(
                undo: () => collection[index] = oldItem,
                redo: () => collection[index] = newItem
            ));
        }


        public static void ClearWithUndo<T>(this ObservableCollection<T> collection, IUndoService undoService)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (undoService == null) throw new ArgumentNullException(nameof(undoService));

            var snapshot = collection.ToList();

            collection.Clear();

            undoService.Do(new DelegateUndoAction(
                undo: () =>
                {
                    collection.Clear();
                    for (int i = 0; i < snapshot.Count; i++)
                    {
                        collection.Add(snapshot[i]);
                    }
                },
                redo: () => collection.Clear()
            ));
        }
        #endregion
    }
}

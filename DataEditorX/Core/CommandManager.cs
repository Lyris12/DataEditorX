﻿namespace DataEditorX.Core
{
    public delegate void StatusBool(bool val);
    public interface ICommand : ICloneable
    {
        bool Execute(params object[] args);
    }
    public interface IBackableCommand : ICommand
    {
        void Undo();
    }
    public interface ICommandManager
    {
        void ExcuteCommand(ICommand command, params object[] args);
        void Undo();
        void ReverseUndo();//反撤销

        event StatusBool UndoStateChanged;
    }
    public class CommandManager : ICommandManager
    {
        private readonly Stack<ICommand> undoStack = new();
        private readonly Stack<ICommand> reverseStack = new();

        public event StatusBool UndoStateChanged;

        public CommandManager()
        {
            UndoStateChanged += new StatusBool(CommandManager_UndoStateChanged);
            UndoStateChanged += new StatusBool(CommandManager_ReverseUndoStateChanged);
        }

        private void CommandManager_UndoStateChanged(bool val)
        {

        }

        private void CommandManager_ReverseUndoStateChanged(bool val)
        {

        }

        #region ICommandManager 成员
        public void ExcuteCommand(ICommand command, params object[] args)
        {
            if (!command.Execute(args))
            {
                return;
            }

            reverseStack.Clear();

            if (command is IBackableCommand)
            {
                undoStack.Push((ICommand)command.Clone());
            }
            else
            {
                undoStack.Clear();
            }

            UndoStateChanged(undoStack.Count > 0);
        }

        public void Undo()
        {
            IBackableCommand command = (IBackableCommand)undoStack.Pop();
            if (command == null)
            {
                return;
            }

            command.Undo();
            reverseStack.Push((ICommand)command.Clone());

            UndoStateChanged(undoStack.Count > 0);
            //UndoStateChanged(reverseStack.Count > 0);
        }

        public void ReverseUndo()
        {
            IBackableCommand command = (IBackableCommand)reverseStack.Pop();
            if (command == null)
            {
                return;
            }

            _ = command.Execute();
            undoStack.Push((ICommand)command.Clone());

            UndoStateChanged(undoStack.Count > 0);
        }
        #endregion
    }
}

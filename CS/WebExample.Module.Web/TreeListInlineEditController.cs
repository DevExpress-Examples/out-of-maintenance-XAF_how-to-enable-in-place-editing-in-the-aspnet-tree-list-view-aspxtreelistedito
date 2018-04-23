using System;
using DevExpress.ExpressApp;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp.Web;
using DevExpress.Web.ASPxTreeList;
using DevExpress.ExpressApp.Web.Editors;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.TreeListEditors.Web;

namespace WebExample.Module.Web {
    public class TreeListInlineEditController : ViewController<ListView> {
        public TreeListInlineEditController() {
            TargetObjectType = typeof(ITreeNode);
        }
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            ASPxTreeListEditor editor = View.Editor as ASPxTreeListEditor;
            if (editor != null && View.Model.AllowEdit) {
                editor.TreeList.SettingsEditing.Mode = DevExpress.Web.ASPxTreeList.TreeListEditMode.Inline;
                editor.TreeList.NodeUpdating += new DevExpress.Web.Data.ASPxDataUpdatingEventHandler(TreeList_NodeUpdating);
                editor.TreeList.CommandColumnButtonInitialize += new TreeListCommandColumnButtonEventHandler(TreeList_CommandColumnButtonInitialize);
                foreach (TreeListColumn column in editor.TreeList.Columns) {
                    if (column is TreeListDataColumn && ((TreeListDataColumn)column).EditCellTemplate != null) {
                        WebPropertyEditor propertyEditor = ((EditModeDataItemTemplate)((TreeListDataColumn)column).EditCellTemplate).PropertyEditor;
                        propertyEditor.ControlCreated += new EventHandler<EventArgs>(propertyEditor_ControlCreated);
                    }
                }
            }
        }
        void TreeList_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) {
            ObjectSpace.CommitChanges();
        }
        void TreeList_CommandColumnButtonInitialize(object sender, TreeListCommandColumnButtonEventArgs e) {
            if (e.ButtonType == TreeListCommandColumnButtonType.Edit) {
                e.Visible = DevExpress.Utils.DefaultBoolean.True;
                e.CommandColumn.ButtonType = ButtonType.Link;
                e.CommandColumn.EditButton.Text = "Inline Edit";
            } else if (e.ButtonType == TreeListCommandColumnButtonType.Update || e.ButtonType == TreeListCommandColumnButtonType.Cancel) {
                e.Visible = DevExpress.Utils.DefaultBoolean.True;
            }
        }
        void propertyEditor_ControlCreated(object sender, EventArgs e) {
            WebControl control = ((WebPropertyEditor)sender).Editor as WebControl;
            if (control != null) {
                control.Attributes["onclick"] = RenderHelper.EventCancelBubbleCommand;
            }
        }
    }
}

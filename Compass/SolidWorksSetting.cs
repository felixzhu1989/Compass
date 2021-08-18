using System;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;
using SolidWorksHelper;
using SolidWorks.Interop.swconst;

namespace Compass
{
    //Wiki地址：http://10.9.18.31:8080/document/index?document_id=129
    public class SolidWorksSetting
    {
        //solidWorks程序
        private SldWorks swApp;
        public async void SolidWorksHaltonSetting()
        {
            try
            {
                swApp = await SolidWorksSingleton.GetApplicationAsync();
                //SolidWorks文件地址设置
                //点击Options—>FileLocations—>Show folders for:下拉框中选择所需要的文件地址—>点击Add..
                //Document Templates模板文件D:\halton\01 Tech Dept\01 Design Library\01 Templates
                swApp.SetUserPreferenceStringValue((int) swUserPreferenceStringValue_e.swFileLocationsDocumentTemplates,@"D:\halton\01 Tech Dept\01 Design Library\01 Templates");
                //Custom Property Files自定义属性D:\halton\01 Tech Dept\01 Design Library\03 Custom Property
                swApp.SetUserPreferenceStringValue((int) swUserPreferenceStringValue_e.swFileLocationsCustomPropertyFile, @"D:\halton\01 Tech Dept\01 Design Library\03 Custom Property");
                //Design Library设计库D:\halton\01 Tech Dept
                swApp.SetUserPreferenceStringValue((int) swUserPreferenceStringValue_e.swFileLocationsDesignLibrary, @"D:\halton\01 Tech Dept");
                //Macros宏(VBA)，D:\halton\01 Tech Dept\01 Design Library\04 Macros
                swApp.SetUserPreferenceStringValue((int) swUserPreferenceStringValue_e.swFileLocationsMacros, @"D:\halton\01 Tech Dept\01 Design Library\04 Macros");
                //Material Databases材料库，D:\halton\01 Tech Dept\01 Design Library\02 Material
                swApp.SetUserPreferenceStringValue((int) swUserPreferenceStringValue_e.swFileLocationsMaterialDatabases, @"D:\halton\01 Tech Dept\01 Design Library\02 Material");


                //设置消除轻化模式
                //点击Options—> Assemblies—> Opening large assembiles:如下图所示取消勾选框内两项
                //1.?
                swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swLargeAsmModeUseLargeDesignReview, false);


                //点击Options—>Assemblies—>Envelope Components:如下图所示取消勾选框内两项
                swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swLoadEnvelopeLightweight, false);
                swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swLoadEnvelopeReadOnly, false);

                //点击Options—>Performance—>Assemblies:如下图所示取消勾选框内两项
                swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swLargeAsmModeAutoLoadLightweight, false);
                swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swPerformanceAlwaysResolveSubassemblies, false);


                //设置OpenGL
                //点击Options—>Performance—>如下图所示取消勾选Use Software OpenGL
                //?


                //允许在模型树中重命名
                //点击Options—>FeatureManager—>如下图所示勾选Allow components files to be rename from featureManager tree
                swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swFeatureManagerEnableRenamingComponent, true);


                //开启动态高亮显示
                //点击Options—>Display—>如下图所示勾选框中的两项
                swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swEdgesDynamicHighlight, true);
                swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swEdgesHighlightFeatureEdges, true);

                //解决视图旋转问题
                //swCommands_View_Zoom_About_Center 1803; View menu > Modify > Zoom About Screen Center 
                //swCommands_e

                //导出eDrawing文件设置可测量
                //点击Options—>Export—>File Format:下拉框中选择EDRW/EPRT/EASM—>如下图所示勾选Okay to measure this eDrawing file
                swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swEDrawingsOkayToMeasure, true);

                //装配体中零件无法用鼠标拖动
                //点击Setting—>Assemblies—>如下图勾选Move components by dragging
                swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swAssemblyAllowComponentMoveByDragging, true);

                //反转鼠标滚轮方向
                //点击Setting—>Views—>如下图勾选Reverse mouse wheel zoom direction
                swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swViewReverseWheelZoomDirection, true);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                swApp.CommandInProgress = false;//及时关闭外部命令调用，否则影响SolidWorks的使用
                swApp.SendMsgToUser("SolidWorks设置已完成！");
            }
        }
    }
}

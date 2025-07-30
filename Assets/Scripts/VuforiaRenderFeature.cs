using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

#if VUFORIA_PRESENT
using Vuforia;
#endif

public class VuforiaRenderFeature : ScriptableRendererFeature
{
    class VuforiaRenderPass : ScriptableRenderPass
    {
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
#if VUFORIA_PRESENT && (UNITY_ANDROID || UNITY_IOS)
            if (VuforiaBehaviour.Instance != null &&
                VuforiaBehaviour.Instance.enabled &&
                VuforiaUnity.IsInitialized &&
                VuforiaRuntimeUtilities.IsVuforiaEnabled())
            {
                VuforiaRenderDelegate.Instance.OnPreRender(context, renderingData.cameraData.camera);
            }
#endif
        }
    }

    VuforiaRenderPass pass;

    public override void Create()
    {
        pass = new VuforiaRenderPass
        {
            renderPassEvent = RenderPassEvent.BeforeRenderingOpaques
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(pass);
    }
}
Thank you for buying my asset I hope it will meet your needs :)!
If you have any questions or problems please contact me piotrtplaystore@gmail.com

How to use this asset:
1. Create PostProcessorSettings asset (right-click in project view Create -> Mobile Optimized Post Processing -> PostProcessorSettings)
2. Add PostProcessor script to the camera to which post-processing has to be applied and in Settings field select PostProcessorSettings you created in the previous step 
3. Configure your PostProcessorSettings in the way you want

How to properly import LUT texture:
To avoid visual artefacts you have to properly import your LUT textures here is how to import settings should look like:
Texture Type - Default
Texture Shape - 2D
sRGB - Uncheck
alpha source - None

Advanced Settings:
    Streaming Mipmaps - Uncheck
    Generate Mipmaps - Uncheck <---- IMPORTANT

Wrap Mode - Clamp
Filter Mode - Bilinear
Format - Automatic
Compression - None   <---- IMPORTANT


I attached example settings and LUT textures in Example Assets folder so you can achieve the effect you want right away. 


You can modify all PostProcessorSettings parameters in realtime from scripts, example code:

public PostProcessorSettings settings;
void Update() {
    settings.VignettePower = Mathf.Sin(Time.time);
    settings.LUTEnabled = true;
}
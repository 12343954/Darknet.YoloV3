# Code in (4 steps) YOLOv3 in C#, Custom dataset, 30+ fps, faster & stable than python | 2020 AI Tech

https://www.youtube.com/watch?v=zQW1BMKHWoE&ab_channel=CoolooAI

<br>

<img alt="" src="https://raw.githubusercontent.com/12343954/Darknet.YoloV3/refs/heads/main/Doc/run.jpg" width="800" />

<br>

<img alt="" src="https://raw.githubusercontent.com/12343954/Darknet.YoloV3/refs/heads/main/Doc/vs1.jpg" width="1024" />

<br>

<img alt="driver456.71+cuda11.1+nvcc10.2.png" src="https://raw.githubusercontent.com/12343954/Darknet.YOLOv3.V2/refs/heads/main/images/driver456.71%2Bcuda11.1%2Bnvcc10.2.png" width="1024">



<hr><br><br>

# 1.  Precautions

1.  After a lot of testing and switching between many driver and cuda versions, found that `CUDA10.2+YOLOv3` are the fastest. Other versions are inefficient and have a serious performance loss.  

    | NVIDIA      | GeForce RTX 2080 Ti<br>GeForce RTX 2060| |
    | ----------- | ----------- | ----------- |
    | Driver      | 456.71 | <i style='color:red'>!important</i> |
    | Driver Date      | 2020/9/30      |  |
    | Driver Version   | 27.21.14.5671  |  |
    | CUDA   | 10.2        | cuda_10.2.89_441.22_win10.exe |
    | NVCC   | 10.2        | <i style='color:red'>!important</i> |
    | cuDNN  | 10.2        | <i style='color:red'>!important</i> |
    | YOLO   | V3 (code for 2019) | <i style='color:red'>!important</i> |

2. Make sure that the graphics driver is not automatically updated to the latest version.  
This guarantees the best performance of YOLOv3.
3. If you get the "yolo_cpp.dll.dll not found" error, you need to recompile the `yolo_v3.dll` with VS2019, not the VS2022.   
4. Watch this: [Install & test YoloV3 on Windows 10](https://www.youtube.com/watch?v=zT8eDXpslXw)

<br><br>

# 2. Usage
    1, Download all the files

    2, Unzip the files by WinRAR via "Extract Here",

        1. /Darknet.YoloV3/cudnn64_7.zip
        2. /voc_custom/backup/yolov3_custom_63000.zip

    3. Change the path in `GlobalSatics.cs`
    4. Start and enjoy it


###  PS: 

    1. I changed the `Alturos.Yolo.dll` and `yolo_cpp_dll_gpu.dll` to fix the issus, 
        so DO **NOT** use the default DLLs by nuget(You can use the nuget install the Alturos.YOLO package，
        and replace the dlls with mine).
    2. The `packages` files is modified by me.
    3. The `example` images are for testing.
    4. The `voc_custom` files are my custom training model.

<br><br>

# 3. Issues Fixed 
    
### Error 1. System.DllNotFoundException - `Microsoft Visual C++ 2015-2019 Redistributable (x64)

    ```
    in `.\src\Alturos.Yolo\DefaultYoloSystemValidator.cs`, line: 51
    add more vc++ versions check, and recompile

    var checkKeys = new Dictionary ...
    {
        ...
        { @"Installer\Dependencies\VC,redist.x64,amd64,14.27,bundle", "Microsoft Visual C++ 2015-2019 Redistributable (x64)" },
        { @"Installer\Dependencies\VC,redist.x64,amd64,14.28,bundle", "Microsoft Visual C++ 2015-2019 Redistributable (x64)" },
        { @"Installer\Dependencies\VC,redist.x64,amd64,14.29,bundle", "Microsoft Visual C++ 2015-2019 Redistributable (x64)" }
    };
    ```
### Error 2. System.DllNotFoundException - Unable to load DLL 'yolo_cpp_dll_gpu.dll': The specified module could not be found.
prepare the `Yolo_cpp_dll.dll`(GPU mode), 
you can follow the tutorial in my channel here  
https://youtu.be/zT8eDXpslXw
1. complied the dll `Yolo_cpp_dll.dll`
2. prepare to replace it to the original `yolo_cpp_dll_gpu.dll`, KEEP with the original name `yolo_cpp_dll_gpu.dll`

### Error 3.  YOLO.Detect(File.ReadAllBytes(imagPath)); //System.NotImplementedException: 'C++ dll compiled incorrectly'

solution is here: https://bit.ly/33jVMLb

<br><br>

# 4. MIT License

Enjoy it!
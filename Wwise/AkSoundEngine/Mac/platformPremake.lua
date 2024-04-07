-- Mac Unity premake module
local platform = {}

function platform.setupTargetAndLibDir(baseTargetDir)
    targetdir(baseTargetDir .. "Mac/%{cfg.buildcfg}")
    libdirs("${WWISESDK}/Mac_Xcode1500/%{cfg.buildcfg}/lib")
end

function platform.platformSpecificConfiguration()
    links {
        "AudioToolbox.framework",
        "AudioUnit.framework",
        "AVFoundation.framework",
        "Foundation.framework",
        "CoreAudio.framework"
    }
	
	xcodebuildsettings {
		GENERATE_INFOPLIST_FILE = "YES",
		PRODUCT_BUNDLE_IDENTIFIER = "com.audiokinetic.wwiseunityintegration"
	}
end

return platform
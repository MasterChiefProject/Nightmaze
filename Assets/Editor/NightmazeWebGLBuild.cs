using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class NightmazeWebGLBuild
{
    private const string WebGLTemplate = "PROJECT:Nightmaze";
    private const string OutputDirectoryName = "docs";

    [MenuItem("Nightmaze/Apply Production WebGL Settings")]
    public static void ApplyProductionWebGLSettings()
    {
        PlayerSettings.companyName = "MasterChiefProject";
        PlayerSettings.productName = "Nightmaze";
        PlayerSettings.bundleVersion = "1.0.0";
        PlayerSettings.WebGL.template = WebGLTemplate;
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Gzip;
        PlayerSettings.WebGL.decompressionFallback = true;
        PlayerSettings.WebGL.dataCaching = true;

        Debug.Log("Nightmaze production WebGL settings applied.");
    }

    [MenuItem("Nightmaze/Build WebGL for GitHub Pages")]
    public static void BuildWebGLForGitHubPages()
    {
        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        if (scenes.Length == 0)
        {
            throw new InvalidOperationException("Nightmaze has no enabled scenes in Editor Build Settings.");
        }

        string projectRoot = Directory.GetParent(Application.dataPath)?.FullName
            ?? throw new InvalidOperationException("Unable to resolve the Unity project root.");
        string outputPath = Path.Combine(projectRoot, OutputDirectoryName);

        if (Directory.Exists(outputPath))
        {
            Directory.Delete(outputPath, recursive: true);
        }

        Directory.CreateDirectory(outputPath);

        ApplyProductionWebGLSettings();

        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = outputPath,
            target = BuildTarget.WebGL,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(options);
        if (report.summary.result != BuildResult.Succeeded)
        {
            throw new InvalidOperationException(
                $"Nightmaze WebGL build failed: {report.summary.result} " +
                $"({report.summary.totalErrors} errors, {report.summary.totalWarnings} warnings)."
            );
        }

        File.WriteAllText(Path.Combine(outputPath, ".nojekyll"), string.Empty);
        AssetDatabase.Refresh();

        Debug.Log(
            $"Nightmaze WebGL build completed: {outputPath} " +
            $"({report.summary.totalSize / (1024f * 1024f):F1} MB)."
        );
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

public partial class UpdaterCheckForm : Form
{
    private static readonly HttpClient client = new();
    private readonly string baseUrl = "https://raw.githubusercontent.com/c-closed/PDF_TO_JPG_converter/main/published";
    private readonly string localDir = AppDomain.CurrentDomain.BaseDirectory;
    private readonly string updaterExe = "Updater.exe";

    public UpdaterCheckForm()
    {
        InitializeComponent();
        Shown += UpdaterCheckForm_Shown;
    }

    private async void UpdaterCheckForm_Shown(object sender, EventArgs e)
    {
        labelStatus.Text = "업데이트 파일 확인 중...";
        progressBar.Style = ProgressBarStyle.Marquee;

        bool needsUpdate = await SyncWithManifestAsync();

        progressBar.Style = ProgressBarStyle.Blocks;

        if (needsUpdate)
        {
            var dr = MessageBox.Show("새 업데이트가 있습니다. 지금 설치하시겠습니까?", "업데이트 확인", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                string updaterPath = Path.Combine(localDir, updaterExe);
                if (File.Exists(updaterPath))
                {
                    Process.Start(updaterPath);
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }
                else
                {
                    MessageBox.Show("업데이트 프로그램이 없습니다.", "오류");
                }
            }
        }

        // 업데이트 불필요 또는 취소
        DialogResult = DialogResult.OK;
        Close();
    }

    private async Task<bool> SyncWithManifestAsync()
    {
        try
        {
            string manifestUrl = $"{baseUrl}/manifest.json";
            string manifestJson = await client.GetStringAsync(manifestUrl);
            Manifest manifest = JsonSerializer.Deserialize<Manifest>(manifestJson);

            var remoteFileNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var file in manifest.files)
                remoteFileNames.Add(file.name);

            // 로컬 파일 삭제 (manifest 없는 파일)
            foreach (var localFile in Directory.GetFiles(localDir, "*", SearchOption.TopDirectoryOnly))
            {
                string fname = Path.GetFileName(localFile);
                if (!remoteFileNames.Contains(fname) && !fname.Equals(updaterExe, StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        File.Delete(localFile);
                    }
                    catch { /* 삭제 실패 시 무시 */ }
                }
            }

            bool differ = false;

            // manifest 기준 다운로드/업데이트
            foreach (var remote in manifest.files)
            {
                string localPath = Path.Combine(localDir, remote.name);
                string localHash = GetFileHash(localPath);
                if (!File.Exists(localPath) || !string.Equals(localHash, remote.hash, StringComparison.OrdinalIgnoreCase))
                {
                    await DownloadFileAsync($"{baseUrl}/{remote.name}", localPath);
                    differ = true;
                }
            }

            return differ;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"업데이트 확인 중 오류가 발생했습니다:\n{ex.Message}", "오류");
            return false;
        }
    }

    private async Task DownloadFileAsync(string url, string path)
    {
        byte[] data = await client.GetByteArrayAsync(url);
        await File.WriteAllBytesAsync(path, data);
    }

    private string GetFileHash(string path)
    {
        if (!File.Exists(path)) return "";
        using var sha256 = SHA256.Create();
        using var stream = File.OpenRead(path);
        return BitConverter.ToString(sha256.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
    }

    public class Manifest
    {
        public string version { get; set; }
        public List<FileInfoEntry> files { get; set; }
    }

    public class FileInfoEntry
    {
        public string name { get; set; }
        public string hash { get; set; }
    }
}

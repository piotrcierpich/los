using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace KoloLos.Models.GalleryManager
{
    internal class GalleryFilesResolver
    {
        private readonly string pathOfFile;

        private string[] unpackedFileNames;

        public GalleryFilesResolver(string pathOfFile)
        {
            if (string.IsNullOrEmpty(pathOfFile))
                throw new ArgumentException();

            this.pathOfFile = pathOfFile;
            unpackedFileNames = new[] { pathOfFile };
        }

        public void UnpackIfArchive()
        {
            if (IsArchive())
                UnpackAndSetUnpackedFileNames();
        }

        private bool IsArchive()
        {
            return new FileInfo(pathOfFile).Extension.Equals(".zip", StringComparison.InvariantCultureIgnoreCase);
        }

        private void UnpackAndSetUnpackedFileNames()
        {
            unpackedFileNames = UnpackAndGetFileNames();
            DeleteArchive();
        }

        private string[] UnpackAndGetFileNames()
        {
            string directory = Path.GetDirectoryName(pathOfFile);
            using (ZipArchive archive = ZipFile.OpenRead(pathOfFile))
            {
                return archive.Entries.Select(e => UnpackAndGetFileName(e, directory)).ToArray();
            }
        }

        private string UnpackAndGetFileName(ZipArchiveEntry entry, string destinationDirectory)
        {
            string pathToUnpackedFile = Path.Combine(destinationDirectory, entry.Name);
            entry.ExtractToFile(pathToUnpackedFile);
            return pathToUnpackedFile;
        }

        private void DeleteArchive()
        {
            File.Delete(pathOfFile);
        }

        public string[] GetFileNames()
        {
            return unpackedFileNames;
        }
    }
}
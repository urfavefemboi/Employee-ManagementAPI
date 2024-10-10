namespace EmployeeAPI.Services
{
    public class UploadFile : IFileUpload
    {
        public async Task<string> Upload(IFormFile file)
        {
            //check image file name
            List<string> acceptfilename = new List<string>() { ".jpg", ".gif", ".png" };
            string pathextension = Path.GetExtension(file.FileName);
            if(!acceptfilename.Contains(pathextension))
            {
                return null;
            }
            long size = file.Length;
            // check imagesize
            if (size > (5 * 1024 * 1024))
                return null;
            //new imagefile name
            string filename = Guid.NewGuid().ToString() + pathextension;
            string path=Path.Combine(Directory.GetCurrentDirectory(), "ImageFile");
            using (FileStream stream = new FileStream(Path.Combine(path,filename), FileMode.Create))
                await file.CopyToAsync(stream);
            return filename;
        }
    }
}

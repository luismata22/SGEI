using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGEI.Interfaces;
using SGEI.Models.Master;
using SGEI.Models.Master.Filters;
using SGEI.Models.Security.Filters;
using System.Collections.Generic;
using System.IO;

namespace SGEI.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class StudentController : ControllerBase
  {
    private readonly IStudentRepository _studentRepository;

    public StudentController(IStudentRepository studentRepository)
    {
      _studentRepository = studentRepository;
    }

    [AllowAnonymous]
    [HttpGet("GetTypeCourse")]
    public ActionResult<List<TypeCourse>> GetTypeCourse()
    {
      List<TypeCourse> result = _studentRepository.GetTypeCourse();
      return new JsonResult(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public ActionResult<List<Student>> Get([FromQuery] StudentFilters filter)
    {
      List<Student> result = _studentRepository.GetStudents(filter);
      return new JsonResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetStudentById")]
    public ActionResult<Student> GetStudentById(long idStudent)
    {
      Student result = _studentRepository.GetStudentById(idStudent);
      if(result.archivosxestudiante.FindAll(x => x.indperfil == true).Count > 0)
      {
        string folderName = @"c:\Archivos";
        string uploads = Path.Combine(folderName, "estudiante-" + result.id);
        result.archivosxestudiante.Find(x => x.indperfil == true).nombre = Path.Combine(uploads, result.archivosxestudiante.Find(x => x.indperfil == true).nombre);
      }
      return new JsonResult(result);
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult<long> Post(Student model)
    {
      long result = _studentRepository.Post(model);
      return new JsonResult(result);
    }

    [AllowAnonymous]
    [HttpPost("UploadFiles")]
    public ActionResult<long> UploadFiles([FromForm] FilesxStudents file)
    {
      long result = _studentRepository.UploadFiles(file);
      if(result > 0)
      {
        string folderName = @"c:\Archivos";
        string uploads = Path.Combine(folderName, "estudiante-" + file.idestudiante);
        System.IO.Directory.CreateDirectory(uploads);
        if (file.file.Length > 0)
        {
          string filePath = Path.Combine(uploads, file.file.FileName);
          using (Stream fileStream = new FileStream(filePath, FileMode.Create))
          {
            file.file.CopyToAsync(fileStream);
          }

        }
      }
      else
      {
        return -1;
      }

      //// Specify a name for your top-level folder.
      //string folderName = @"c:\Top-Level Folder";

      //// To create a string that specifies the path to a subfolder under your
      //// top-level folder, add a name for the subfolder to folderName.
      //string pathString = System.IO.Path.Combine(folderName, "SubFolder");

      //// You can write out the path name directly instead of using the Combine
      //// method. Combine just makes the process easier.
      //string pathString2 = @"c:\Top-Level Folder\SubFolder2";

      //// You can extend the depth of your path if you want to.
      ////pathString = System.IO.Path.Combine(pathString, "SubSubFolder");

      //// Create the subfolder. You can verify in File Explorer that you have this
      //// structure in the C: drive.
      ////    Local Disk (C:)
      ////        Top-Level Folder
      ////            SubFolder
      //System.IO.Directory.CreateDirectory(pathString);

      //// Create a file name for the file you want to create.
      //string fileName = System.IO.Path.GetRandomFileName();

      //// This example uses a random string for the name, but you also can specify
      //// a particular name.
      ////string fileName = "MyNewFile.txt";

      //foreach (var file in files)
      //{
      //  // Use Combine again to add the file name to the path.
      //  pathString = System.IO.Path.Combine(pathString, file.FileName);

      //  // Check that the file doesn't already exist. If it doesn't exist, create
      //  // the file and write integers 0 - 99 to it.
      //  // DANGER: System.IO.File.Create will overwrite the file if it already exists.
      //  // This could happen even with random file names, although it is unlikely.
      //  if (!System.IO.File.Exists(pathString))
      //  {
      //    using (System.IO.FileStream fs = System.IO.File.Create(pathString))
      //    {
      //      for (byte i = 0; i < 100; i++)
      //      {
      //        fs.WriteByte(i);
      //      }
      //    }
      //  }
      //  else
      //  {
      //    //Console.WriteLine("File \"{0}\" already exists.", fileName);
      //    //return;
      //  }

      //  // Read and display the data from your file.
      //  try
      //  {
      //    byte[] readBuffer = System.IO.File.ReadAllBytes(pathString);
      //    foreach (byte b in readBuffer)
      //    {
      //      //Console.Write(b + " ");
      //    }
      //    //Console.WriteLine();
      //  }
      //  catch (System.IO.IOException e)
      //  {
      //    //Console.WriteLine(e.Message);
      //  }
      // }
      return result;
    }

    [AllowAnonymous]
    [HttpGet("GetFileByIdStudent")]
    public ActionResult<List<FilesxStudents>> GetFileByIdStudent(long idStudent)
    {
      List<FilesxStudents> result = _studentRepository.GetFileByIdStudent(idStudent);
      return new JsonResult(result);
    }

    [AllowAnonymous]
    [HttpPost("DeleteFile")]
    public ActionResult<long> DeleteFile(FilesxStudents file)
    {
      long result = _studentRepository.DeleteFile(file);
      if (result > 0)
      {
        string folderName = @"c:\Archivos\estudiante-" + file.idestudiante;
        string uploads = Path.Combine(folderName, file.nombre);
        if (System.IO.File.Exists(uploads))
        {
          System.IO.File.Delete(uploads);
          return result;
        }
        else
        {
          return result;
        }
      }
      else
      {
        return -1;
      }
    }
  }
}

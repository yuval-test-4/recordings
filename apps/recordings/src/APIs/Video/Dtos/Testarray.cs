using System.ComponentModel.DataAnnotations;
using Recordings.APIs.Dtos;

namespace Recordings.APIs;

public class Testarray
{
    [Required()]
    public Testarray Keyvalues { get; set; }
}

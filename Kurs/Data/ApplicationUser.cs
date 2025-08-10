using Microsoft.AspNetCore.Identity;

namespace Kurs.Data
{
    public class ApplicationUser : IdentityUser
    {
        // �������� ���������� ��� ApplicationUser
        public string? FullName { get; set; } // ����� ��'�
        public string? Role { get; set; } // ���� �����������

    }
}

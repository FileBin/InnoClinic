using System.Globalization;

namespace AppointmentsAPI.Tests.Helpers;

public static class Config {
    public const string PatientUserUUID = "06143795-97e6-451c-91f1-4cfc6bbdd2f6";
    public const string PatientEntityUUID = "9faace98-9bd3-4e62-a80f-fda028290f6b";
    public const string DoctorUserUUID = "aa965941-defb-4dc1-8a28-9c281e7d23d7";
    public const string DoctorEntityUUID = "e0ccd817-4937-47db-97bf-610ab9bffa5b";

    public const string AppointmentUUID = "f7e30427-73df-48bd-8e5a-53396ca2fdcd";
    public const string ServiceUUID = "e6a6c825-a427-4c87-a408-d0bc684e0966";
    public const string SpecializationUUID = "05f3a96e-fd3f-485d-bbb8-67e2e7fc3ad9";
    public const string OfficeUUID = "e83bcebe-69d9-48f5-83dc-29e97ec4f0e6";

    public static readonly CultureInfo CultureInfo = CultureInfo.GetCultureInfo("en-US");
}

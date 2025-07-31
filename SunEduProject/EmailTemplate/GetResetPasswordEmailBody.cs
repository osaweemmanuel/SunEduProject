namespace SunEduProject.EmailTemplate
{
    public static class GetResetPasswordEmailBody
    {
        public static string GetEmailResetBody(string resetUrl, string FirstName,string LastName)
        {
       return $@"
        <div style='font-family:Arial,sans-serif;max-width:600px;margin:auto'>
            <h2>Hello {FirstName} {LastName},</h2>
            <p>You requested to reset your password.</p>
            <p>
                Click the button below to reset it:
            </p>
            <a href='{resetUrl}' style='display:inline-block;padding:10px 20px;
                background-color:#007BFF;color:#fff;text-decoration:none;
                border-radius:5px;'>Reset Password</a>
            <p>If you didn’t request this, you can ignore this email.</p>
            <br/>
            <p>Thanks,<br/>SunEdu Team</p>
        </div>";
        }
    }
}

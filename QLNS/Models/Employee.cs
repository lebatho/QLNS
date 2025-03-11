using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS.Models
{
    public class Employee
    {
        public Guid Id { get; set; }

        public string? Address { get; set; }
        [Column("changed_by")]

        public string? ChangedBy { get; set; }

        public string? Code { get; set; }

        public string? Creator { get; set; }
        [Column("date_change")]

        public DateTime? DateChange { get; set; }
        [Column("date_created")]
        public DateTime? DateCreated { get; set; }
        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        public string? Education { get; set; }

        public string? Email { get; set; }
        [Column("full_name")]
        public string? FullName { get; set; }

        public string? Image { get; set; }
        [Column("issuance_date_identity_card")]
        public DateTime? IssuanceDateIdentityCard { get; set; }
        [Column("issued_date_medical_insurance")]
        public DateTime? IssuedDateMedicalInsurance { get; set; }
        [Column("issued_date_social_insurance")]
        public DateTime? IssuedDateSocialInsurance { get; set; }

        public string? Major { get; set; }

        public string? Nation { get; set; }
        [Column("number_id_card")]
        public string? NumberIdCard { get; set; }
        [Column("number_medical_insurance")]
        public string? NumberMedicalInsurance { get; set; }
        [Column("number_social_insurance")]
        public string? NumberSocialInsurance { get; set; }

        public string? Phone { get; set; }
        [Column("place_of_grant_identity_card")]
        public string? PlaceOfGrantIdentityCard { get; set; }
        [Column("place_of_issue_medical_insurance")]
        public string? PlaceOfIssueMedicalInsurance { get; set; }
        [Column("place_of_issue_social_insurance")]
        public string? PlaceOfIssueSocialInsurance { get; set; }
        [Column("quit_job_date")]
        public DateTime? QuitJobDate { get; set; }
        [Column("refusal_reason")]
        public string? RefusalReason { get; set; }

        public string? Religion { get; set; }

        public string? Sex { get; set; }

        public int? Status { get; set; }
        [Column("certificate_id")]
        public string? CertificateId { get; set; }
        [Column("contract_id")]
        public string? ContractId { get; set; }
        [Column("department_id")]
        public string? DepartmentId { get; set; }
        [Column("additional_request_content")]
        public string? AdditionalRequestContent { get; set; }
        [Column("image_name")]
        public string? ImageName { get; set; }
        [Column("candidate_profile_id")]
        public string? CandidateProfileId { get; set; }

        public string? Note { get; set; }
        [Column("title_recruit")]
        public string? TitleRecruit { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IcMWebApi.Utilities
{
    public class Queries
    {
  
        public const string AppBelowQuery1 = @"SELECT tr,fabric_name AS target_tenant_ring, weight, vm_size, placement_affinity_tag as affinity_tag, multiaz, placement_preference_tag as preference_tag,
                                                    CONVERT(int, IIF(placement_preference_tag_isolation_flag = 1, 1, 0)) as preference_isolated
                                                FROM
                                                (
                                                    SELECT
                                                        tr,fabric_name, placement_base_weight as weight, vm_size, placement_affinity_tag, placement_preference_tag, placement_preference_tag_isolation_flag, multiaz
                                                    FROM
                                                    (
                                                        SELECT
                                                            CAST(SUBSTRING(str.name, 3, CHARINDEX('.', name)-3) as int) as tr,
                                                            str.name as fabric_name,
                                                            str.placement_base_weight,
                                                            str.placement_affinity_tag,
                                                            str.placement_preference_tag,
                                                            str.placement_preference_tag_isolation_flag,
                                                            str.current_counts.value('(/Roles/Role[contains(@name, ''DB'')]/@vmSize)[1]', 'nvarchar(32)' ) as vm_size,
                                                            (CASE WHEN str.current_counts.value('(/Roles/Role[contains(@name, ''HS'')]/@vmSize)[1]', 'nvarchar(32)') is not null THEN 1
                                                            ELSE 0 END) as multiaz
                                                        FROM
                                                            sql_tenant_rings as str
                                                    ) AS T
                                                ) AS X where X.VM_size = '{0}' and X.placement_affinity_tag Is Null and X.placement_preference_tag Is null and X.multiaz = 0 and X.weight = 1 ORDER BY TR";



        //{tenant_ring_name}  {0}
        #region AppBelowQuery2
        public const string AppBelowQuery2 = @"MonAnalyticsDBSnapshot | where PreciseTimeStamp > ago(2h) and isnotempty(tenant_ring_name)
                                                    | where isnotempty(sql_instance_name)
                                                    | extend unsafe_sub = customer_subscription_id in (""00689578-60a6-4cbd-ba61-b367b1170b71"",""041c044a-ae74-442f-8b63-b5e1e2cb24a9"",
                                                    ""042e2e79-7bb9-4031-adcc-1b9f7f747848"",""04b9beef-30b6-4a1a-b5cf-3106c613d5b0"",""051d57a5-194d-441a-833c-b420a54a7b58"",
                                                    ""065738cf-c320-442c-baa4-0df510a7c90c"",""0663d77f-b931-4d1b-978f-dd55b307a577"",""06d899d5-1c83-4505-ad85-0002d5a125ac"",
                                                    ""086a103d-537c-467d-83cb-7c4bd159a998"",""08da5550-9bf6-48ca-be51-b2426579709e"",""09d29343-ed9a-4ad8-baa3-25e147d2d48a"",
                                                    ""0bbef18d-f050-47b4-a17f-8755f4884de4"",""0bd27d10-bf58-4703-96d9-79a927718895"",""0bd7bb0d-ff53-4e17-9caf-66fb53986c7b"",
                                                    ""0c86ab59-b0cd-4ecf-acc2-358523d57082"",""0c8ee94f-e9a1-4b8a-8bf5-12845b6c165b"",""0d33adff-ae30-4abf-a99f-02b3d786d6bc"",
                                                    ""0ef2bc17-63af-4c54-bcf4-fb84436304c8"",""0fc829bd-daf2-40c8-a952-515c8eeab0ed"",""0ffa90b2-4a7a-4952-9ca5-bbfd7d437d0f"",
                                                    ""1048eeee-64a4-448f-a47e-42f442c89370"",""115a2a58-a92c-441e-8e31-77231644c786"",""1163c1f2-4c61-40d4-b17c-ea83a2c1f9ab"",
                                                    ""118a6bec-58c3-4409-ab51-e54745235307"",""148acb51-68f1-4b48-86df-af70ffc46e6f"",""14f0e1cc-6412-4598-bb16-43bbd459c868"",""15693d7b-92ac-43ca-a03b-cbc82b558972"",
                                                    ""156dcc67-6270-489d-9536-b75c43a2e80f"",""1695d0d1-5cd3-46cd-a3da-cf587492edea"",""189b67fa-c6a5-4ca5-a967-874f8c4b26ef"",""192f316c-7515-49db-a399-ae041518faea"",
                                                    ""19e28071-4309-4ccf-849c-3dc88e0be4a2"",""1a3756dc-e752-4900-a515-d5d9ee0de280"",""1aec8114-6c14-4876-a98b-eb2399da70cf"",""1ba99170-9ef3-415a-a523-c21f829a0e5a"",
                                                    ""1c902d5c-bb5c-450f-a2ce-e7b978f6cdee"",""1cf1875c-b039-4bdc-b79d-68639d76f700"",""1cfc6b76-7be0-4225-a673-ad7918dfad8b"",""1dd78362-86ed-4876-adb4-169454b185e3"",
                                                    ""1efaad03-9584-4aae-a139-92fbe4caaa3d"",""21f598c2-7ba5-498f-a19e-4db91719d2e4"",""22249ba4-6018-4fca-9fcd-d6ba523336b1"",""23314bf5-c93c-4fbf-a4b1-2ac84cdd4d65"",
                                                    ""2401088c-bc6d-43ce-a89b-f5ad4620829d"",""255a865b-1f63-4bc1-bb6c-909553a0aebe"",""26987fd4-73e8-4fc8-8b3b-4e3d387bf1d8"",""26f57280-9b84-42d5-ae72-ecfb510a716b"",
                                                    ""27839b58-9325-42c9-95c3-888433dd40a1"",""2848f5fc-106d-4c62-954d-d03f8d21e47e"",""2a89ec63-0483-4f3a-b5cf-a3851475c73b"",""2becab7d-2aaf-49ec-9824-21381e41aa08"",
                                                    ""2d5dbe2e-7e52-4246-aa33-2da329d24d61"",""2da2b5d4-44d5-4263-848e-1db841c6ad11"",""2dd6c30d-7cd4-499b-a8cc-6b5b317f66b6"",""2e673f15-4b24-451f-a57e-a35552360f30"",
                                                    ""2e948937-2df4-4755-9caf-e73058b9163a"",""2f49a07b-ad68-40b8-9075-d45025ab28fd"",""2fca88f2-dccc-4fd1-a465-fe8925106990"",""30b1d05e-627c-4db2-a1af-2c5514d2307b"",
                                                    ""315a3587-b451-46f4-a5a7-c07a2f826612"",""31aaeef1-8257-4620-a962-5a0f87e19c19"",""31cdcf2f-0bec-4273-ae10-785c961f56e2"",""3496166e-c457-45fd-b2c0-697efc1928e9"",
                                                    ""34ff46a1-0c85-4ede-94ad-84b70e89205b"",""35650b4e-0c20-4d05-90af-552acd048ccd"",""35d7bc6b-580d-4327-9554-2e44ea98914e"",""36590804-a084-4516-88cc-d523eab70dd7"",
                                                    ""36d37657-19a1-42ef-8fcc-34144ef7ee0d"",""3830d370-6d77-4a96-a3a2-d0685f5b89ac"",""39cd8f77-3baf-410d-90bc-47040250de3e"",""3b03c6c4-2599-4f46-a415-c40da0386d47"",
                                                    ""3b7b427a-aaa4-4c1c-846e-6d13db26f154"",""3c18ad31-07be-4a6b-a1d5-35caf0205b0a"",""3c92854b-ba60-4492-96ee-d1a6d63c9e73"",""3d93052c-fc8b-4c62-991e-18a9a072b201"",
                                                    ""3fbff83a-eb62-4f4d-b4ad-b078b4125ad9"",""4008f612-e161-470c-9f7a-bb3050991223"",""401fa376-a719-4615-a13c-0150433fcdf4"",""40d5883d-d5c5-4cd8-8e26-06b5e6a879f2"",
                                                    ""42ab0613-6c93-4cc7-b500-e1a1a3793f6a"",""44b7ec56-821e-430a-b2fb-1132d7c65fd1"",""44eda6e3-4625-467f-9e0d-1e71be8df849"",""44f560f6-c054-432f-92df-1129760d3a3d"",
                                                    ""45c1abc5-8ad8-489a-ad1f-05d054f228d9"",""461aa15a-13d4-46c9-98bd-36a5d256871b"",""466deeaf-306a-4327-9ad5-38d576a59398"",""46ec6ebd-66e8-43eb-ae19-df5d092a22c8"",
                                                    ""474eba22-0ce0-4b20-8815-d56b04aaba88"",""478513e8-ba06-4c2b-89dc-a772fdcc0cfe"",""479ab15d-b97b-4c20-99fa-869075cd52ae"",""481e7ef0-14b4-41ff-bf8e-f5c6d0e67954"",
                                                    ""483b2f99-58fd-4328-93bc-ba44ef3ee473"",""48965e6f-9d29-43b3-99b3-1608af400113"",""48eb139d-f0d2-4393-b678-133123129927"",""493b91ec-0d9e-455f-a75d-e16d60e5011d"",
                                                    ""49584fd0-f943-4dbb-a271-fb786721143b"",""49ccc8ca-a589-4b60-9044-ff4db56629a1"",""4a889966-82ab-4e21-9852-fa4e5cb82791"",""4aa68bef-46c6-4fb5-80e4-65c065b6d89c"",
                                                    ""4b18e3da-fcd0-43fb-ac1d-e78bdd47eecb"",""4ce9eead-e910-414f-b976-5faa1a5988bb"",""4cf7bcd7-714c-4d61-991a-04bef3cecdc3"",""4d6fd4a8-d060-4e4d-9213-ff4f6611bd23"",
                                                    ""4dc668c8-2c88-4f8d-802b-654debc027ba"",""4f1d8c54-81ea-4ed6-beca-2b57be439dd7"",""4fe82a13-eb65-4e70-bf6a-19cb9d6b9b4c"",""50f0a780-d0f6-4a43-9aad-233e533c9c2d"",
                                                    ""50faf03e-e2bd-4a10-9403-8ae5fa8c60aa"",""51cc75f7-8c09-48c5-a810-abf23dc55401"",""51dbb16f-c0c8-41e3-aea1-93f6e8573e73"",""520ea6cc-8cc7-4983-b8e6-7a32ef62bd84"",
                                                    ""536d225c-03f0-44e2-b792-599a0acb7d1b"",""56246d32-be3f-4f50-8611-400fa00f28e0"",""5736b0e6-6932-4a38-8196-f46c1ed28af5"",""5851f179-8988-4603-aa43-13332359f68d"",
                                                    ""58e7a9e1-919b-4b46-8a84-2a845f291430"",""5ba02e19-4640-411f-9395-76599758e6fd"",""5c3cd719-6814-441c-8ba0-ae2bb742304e"",""5c6b4c60-ddd9-43c0-bd1e-58ed404f7824"",
                                                    ""5d6cedea-b492-4e33-bc04-7fde2f52b256"",""5d70e10d-d45a-457c-a14c-a5cd659fc734"",""5e0dc946-ed03-426a-a1d7-f418de6caf24"",""600a680f-02e9-45e9-86d7-a155bb96f7fc"",
                                                    ""6032ff4e-1b75-4818-bc9b-950da36e01db"",""6137ec52-8a5c-4041-abaf-71f1ebeec4a8"",""6225902b-8db2-4720-af2a-f67b0e46ee26"",""62bd5014-67fd-49dd-a5c1-e1e5c8bab1c1"",
                                                    ""62e10a63-5e6b-42e2-8d99-abfda463d757"",""65d9425e-92e9-4d44-843a-bb03eeb1597c"",""674569ed-0434-468d-b358-a824a66ec898"",""678b1765-c3e0-45f1-95cd-b9831a4f3cc1"",
                                                    ""679c209e-ea08-4f35-86f0-27d60082edf9"",""685ec699-f360-45db-b22e-f679f8490790"",""68c78007-c479-4baf-ae05-fd7b6c35af77"",""6b6774c4-8b89-4f4b-a064-5c8e49ae5d1f"",
                                                    ""6c281187-e69f-48f6-a9cd-1f5e0a4f4437"",""6d6e3669-38a4-436c-a97b-1c62cae5ea66"",""6e2af308-a337-4ed8-9ff0-59b53fec73f2"",""6ede4940-ce92-41ff-b017-88ee12b7b494"",
                                                    ""6fd7cdd5-6985-409b-a9ec-0a733b802ad6"",""6fed01fe-9f23-48cb-9f33-9e4ab841af4e"",""70973a33-6735-497a-bdaa-acde73fb2378"",""71c9418e-9cc5-4bc5-97b0-202078faf441"",
                                                    ""728e05ca-0645-4738-866a-1e0027472f53"",""733f6af4-0708-44d3-9d3d-607d28f2415f"",""73ed9af2-d0ff-4b54-be12-719f560d0c21"",""741ba92b-fd97-48a4-beac-5d35806816f5"",
                                                    ""77be186f-91cb-4acc-947a-aa3903013167"",""77e07de1-f68b-425a-a851-77e687de3c4a"",""7b696382-414c-4283-a8c5-435bf00e012e"",""7d45dd19-455f-43da-bcfe-bd1be905be7f"",
                                                    ""7ed20771-a962-4f76-aa40-4485ba6aa497"",""7f452cba-3bcc-4cef-9cba-9b35bb925e84"",""7f590bc1-9cf8-469f-b5bb-56b785b92f31"",""7f767153-63c4-4ea8-97aa-28c5ad38bc43"",
                                                    ""7fcdf688-6868-48a7-9af1-4bda232b2fb7"",""7fcfbba5-895e-4b6f-97fa-8369ff25bb4c"",""80a89c71-23d0-4eed-b6fb-0e345fe1fb63"",""82530b8d-e7a9-4126-87c5-69883feb40ed"",
                                                    ""826ed7f4-9335-4496-a499-e6fc1dfbbba4"",""8403da61-2731-4c0e-a00c-1ecd41e0d247"",""84d26a45-f181-4453-9e8f-ea2693ae0770"",""860a8b08-7402-4f87-b6dd-b10997b65f6f"",
                                                    ""88130683-4ad2-4efa-92bb-2832720456bc"",""882014f2-55d2-42d8-938a-4b6c80d4f78f"",""882d970f-50f3-410c-b40b-b9a07fdbdc95"",""885b5e5e-5be1-4d5b-9d7b-cb3264b1b848"",
                                                    ""891e8750-cfee-4d72-aeb6-125a0a332365"",""89fccf12-ecee-4416-ad0d-e0edee3a2025"",""8a612eb9-e5fe-40aa-9507-bf7ae4c4b1a7"",""8a69a5c8-f815-475c-853c-375ca9b221c1"",
                                                    ""8a9f33d7-ae0d-4b49-b186-ca33b0d441c0"",""8aad9c76-157a-4b93-b319-a0e478205cbf"",""8b63d1c3-00e5-4eaf-a986-4e3b50d169fc"",""8f570532-3550-40ae-9c86-07541f2486f4"",
                                                    ""8f7454c7-ab0a-44b9-8230-aea3caf6b315"",""929a66f3-0ba8-48b0-94e8-35e5901e98f1"",""93bf6948-40e2-40fd-b14b-3692436006e7"",""941a795f-065d-4530-9d08-ef8a5972bd07"",
                                                    ""941cd805-b870-4d1a-9050-d4314065ec9c"",""941e2a54-e0ae-4de2-b72e-7a86664593ac"",""94bd84cd-5e4c-4d84-93a6-18206ba4ea03"",""94fe0e39-9c47-43fa-ac7a-618514defb76"",
                                                    ""9614fc94-9519-46fa-b7ec-cc1b0411db11"",""96e06d4f-0663-44fd-92df-f38bebf1e9a2"",""97752023-53c4-4d70-9a66-96239365fa9a"",""97b05922-031f-4c6e-8873-a3fae2f6a4da"",
                                                    ""99179c0a-197c-48aa-8712-339c181b65c5"",""991c3726-3c18-42f1-840c-483423c1160c"",""9935cd2d-0126-474f-867a-c53c8ee53a4a"",""99955f46-1494-43ea-abb0-1112f7e10417"",
                                                    ""9a30d4fe-c345-4b57-b303-0aeea3bd6115"",""9a55e080-5092-4665-9c69-19e87634a954"",""9aaad42c-b3b1-45d1-8c8b-6682a1112829"",""9ac21d1c-b438-4381-9c4d-743d45283e46"",
                                                    ""9b205bef-e2d9-4d12-84fd-cbef3179d1d9"",""9b6df863-e108-497f-ad3c-7c89d668c78c"",""9cba4390-1edb-4b72-a29b-d2aec1a45968"",""9d7cf98b-763b-4a18-8133-d29286b4eabb"",
                                                    ""9d943059-e16f-45aa-aa3d-d7d79e470a66"",""9d9b9a27-3328-4c58-843d-0dc0003c3fbf"",""9dbdf8ba-2b97-48fc-9077-48887b54b63c"",""9defa5c1-8a0c-4b4e-9a90-d1a73926b5fc"",""9f9b0a3e-e6e7-4155-b2c3-d0e740c121b2"",""9fd77605-91f7-4d52-ab4c-647ddd42daee"",""a0255ce5-867d-4e31-a977-e6e41bbab90e"",""a0c0ddda-6ecc-46e9-81c4-e6bf35ad4e4d"",""a522ad86-4767-440a-90cb-fa51df576e53"",""a6a330e2-621e-46ec-90b2-1ae0b0eff8b4"",""a6ac01da-7baf-4ef2-bd16-66f2a6c629e8"",""a6b9ccb1-dfb6-4b64-ba7a-21c2cf875f84"",""a6f11dac-25bf-4364-8e3a-1bc284370f8b"",""a74f3997-0f9e-42ac-9dce-058d19deedc2"",""a7efc3ec-829d-4bb7-841b-55a7506088db"",""a813d4c9-9281-4c92-b9a4-5e6805ec6bde"",""a815357d-3a0b-49bc-9ff4-1c43000fe0cf"",""a84eafee-1ff3-43f4-b91e-d0ceb975fca6"",""a97047b8-4078-4abd-974b-6c0911b76a9f"",""aa7e0e8f-96cf-4776-b6b3-5613acfc406d"",""aae1d03e-b462-4c4f-b94c-735b69bd45fe"",""ab16be2e-33a3-4f17-b89b-b873b15beebd"",""abc76cf6-fb6d-45fa-8e7b-e0bef2bf8c26"",""ac715289-aee6-406e-b834-491766167aaa"",""af58ba63-c0fa-4a75-91e1-3d97cd401cb9"",""b08d4cd7-7679-4337-9312-919729fc0dd0"",""b0f3a4a6-4e49-4f5a-9817-0f33bdf204d6"",""b1c04da9-3b05-484e-8076-aa742e0e847f"",""b2aa4b87-7b0b-4e04-94dd-c81baa10da05"",""b402926d-079f-4bd2-984a-899ce88b69dc"",""b4504be0-2c68-414e-81d3-c8ea21f8984b"",""b4bca494-8aa7-434d-826a-10ec453baf3c"",""b4edd648-f1f3-4f3e-b070-cf9a64621e75"",""b5a3dc4a-9c99-4fce-9bb9-2a173c3a804b"",""b5d8337d-7ea2-47af-9653-f8469ac9e059"",""b6831565-39ea-4f2d-887e-c722b0d91f74"",""b7b4d6e8-3dc4-49e1-9ca0-f289ed02f521"",""b7c0d27b-7ca7-46bf-b78b-8ac83ff5a1d2"",""b97df8df-1754-40ca-acc9-0b48492fb75e"",""bafab2d2-4b5f-4c95-aa45-4180a7be8ee7"",""bbd1c169-c343-48c7-b086-5d0c2ed74990"",""bc914e88-9854-4b8a-bb9f-0c9ee7e542a0"",""bcbf516d-5f49-48bb-b86b-a12f96cd676d"",""bd8996b1-27dd-410a-967c-45a3ee0891e2"",""be4ff81b-d22f-435e-bd72-f94f9941717e"",""be5091b0-bc1e-4b61-b2f9-b1baae267b13"",""bfec85b3-3114-4085-acf4-de67863d9fa5"",""c0d89763-ae01-4cd5-a57d-00b8105dc25e"",""c2bca8a3-21fe-4128-b318-2ac9c9065d8c"",""c38d8ae4-cabd-48e3-bcdc-9e459f6cf09e"",""c5afd6a1-b063-4aa2-8d44-b443156d9b14"",""c6956de1-de9a-438b-b54c-3f667a864ef8"",""c863df9d-2736-4d16-8bca-c685141a6c8e"",""c867add5-c3bf-4361-bd0e-ae48f4aa83b7"",""c91c6d9b-60f2-42c3-9323-3b477da232b6"",""c99d1cb6-293c-42f8-bfc9-90a8a9a81461"",""ca246869-ca4a-4547-b728-a171374170a5"",""ca3b8140-fd1f-4878-8fc7-6dd4f190a793"",""cbd0debb-cc24-4fba-bad3-116f6af3b858"",""cbe296f4-c791-455a-a025-4543d8d0a22b"",""cc1741d1-8f2a-410a-9164-65a047996519"",""cd364491-4dfb-4f63-9c52-b0a22e968822"",""ce33c430-6aba-4815-88b0-1abbaa4b6a76"",""d0112df8-f2c4-442b-9005-e2dfb711327d"",""d0e0f288-4fdd-43bf-8285-76ce569cde0e"",""d14f962c-c5a0-4563-8be2-2d9d29e4243a"",""d3986f66-5c10-495b-9fde-ba897a10b4f7"",""d4ede808-4a4d-464b-9d90-a13dd4c3c305"",""d53c6d37-1875-4ee8-9ab2-8ad79be98344"",""d68cf66e-d8e1-4b16-a854-a5dbc5e7ab40"",""d6b539cf-affd-4aae-9a0b-1a3469049bd7"",""d74efa82-4c0c-4ed9-9dc0-f3f0ec8b7da3"",""d7c4ea37-6fb7-465b-a153-1f1dd968b286"",""d7e18f7c-075f-44fa-a909-56dd772a1edb"",""d838d301-80ef-4a50-ac2b-0f4a7f800ef4"",""d8517b43-8ad4-461a-ac40-de475cbd86d6"",""d8e00388-99e2-4f9d-b8fa-ff0d4bdac2c8"",""d8f65e59-9761-47f6-9af7-a4c099a981a2"",""dc55752a-d5b7-4170-8371-a2456520799e"",""dea2f5b1-48dc-4b60-bcaa-cc064c05b9d6"",""dfbb6f84-8e85-42f9-862b-10d778e0b4a5"",""e08eaa6b-0f7f-4c68-a006-898d73dc79d4"",""e0c7e37c-f166-4e38-884d-dcdf0e8f7518"",""e0e477c2-a6a9-4783-8899-4aa1c201ddba"",""e132dd24-2dc8-4660-96db-9204dd73ab48"",""e249092f-02d3-445c-82f5-8be82dfcea9d"",""e3704ee9-8c83-4a14-8d67-f81fce14239f"",""e3bd77cb-acd8-4882-8010-234022f51f21"",""e5323706-4762-423c-ac39-3a27e66a1d35"",""e5b6f565-e525-4a0d-af1a-44f93926e226"",""e6d0ce27-036a-4a83-b119-0487065c3a2f"",""e71eae06-2765-41fd-a3ee-ec4f0b482703"",""e7ca9c2b-d4f9-4cc0-908b-11b59e4bfeac"",""e83bd900-a393-4569-9b2d-b9cc90856c67"",""e87bad11-bde3-4df3-8cc9-1a9782fe1ba3"",""e8ac8746-cc95-4aa6-8979-f62ecab2fe12"",""e9d3203a-6548-49f6-b293-a352aadf077a"",""eafa5266-133f-4117-9338-cb83723f8a48"",""ec9e6107-9985-4685-b97c-769c65805aa1"",""edefbf53-dd87-48f6-baf9-a01c6f66e24e"",""ee691273-18af-4600-bc24-eb6768bf9cfa"",""efa2d86a-9035-4c2f-9b5a-4102a574ecf7"",""f05f5dd0-f018-4be8-90d9-6b65bdefd83f"",""f0bde95b-37c5-42c9-a161-334a30db527e"",""f18f24f5-bd3b-49cf-9163-7b01a822ee64"",""f1f5749f-61b8-4211-81fd-9b01112a3913"",""f21b0765-678a-405e-8476-2f381d8dfa35"",""f24fd476-0545-404a-8960-d7836c5cacd5"",""f2c67f91-e0eb-459c-b95e-d56c7b73b1b6"",""f3c4c51c-2e40-438c-ab95-dabd6e78b066"",""f4309ab0-cc21-4766-87a4-452d068273a3"",""f4c33337-f0f2-4fa3-8ef3-fd050e23927c"",""f51a5e5b-8561-41cd-9bd1-bf9f5e4e7898"",""f5261f82-adb5-4a60-8bde-ca6690ba8737"",""f5a7bd28-87ac-4e69-ba60-9f0a3daa2024"",""f5dc650e-fdab-4ea9-8151-5ab6f726b51e"",""f600f9de-b1d5-4851-aa60-393d6c9f52cc"",""f63bde33-bad7-4fdd-91ff-54df608055cc"",""f721b032-8ef8-44b2-bba0-3232bbc724f3"",""f7d76cf8-d10b-448d-8631-edf197381afc"",""f7f37631-9303-47fe-94c8-8351b3103434"",""f8fd3f8e-49d5-4c8e-8d21-f7e0bbbde0df"",""f90225bd-03a6-4c7d-be8b-fc36cad0a8a4"",""fa2cc3f3-d1e3-4003-96d3-8869a00834bb"",""fa8ee109-be57-4b9e-8115-ecc5877addbe"",""fb3ec954-48f1-4ec2-ad24-0efb23ef9869"",""fb593fb8-f064-4af4-ae98-70bc0f4583f4"",""fbb2d58d-a276-4d8c-a236-94132d8eb867"",""fc8d1c71-f833-4ace-a48b-937a72159ada"",""fca11e34-3e35-4627-8694-3e7ea439f1d2"",""fcf7709a-a453-4682-ba9d-c364e6edd6cb"",""fd935a67-3699-4ef0-ab00-190a135f52ff"",""fd978066-1cc8-49d5-86d3-b247b400f564"",""fe870987-0408-4e9f-87fe-319e398aec42"",""ff595795-d8fa-4082-a20b-f67fea0f7482"",""ff7bfac8-7b5a-4cb5-b256-21502eb62ae0"",""ff7e4c03-77e5-439f-8c79-0acfb00741e0"")
                                                    | summarize (last_time, pool_id, tenant_ring_name, slo, unsafe_sub) = argmax(PreciseTimeStamp, logical_resource_pool_id, tenant_ring_name, service_level_objective, unsafe_sub) by 
                                                        server_name = logical_server_name, logical_database_name, sub_id = customer_subscription_id, AppName = sql_instance_name
                                                    | join kind= inner (
                                                       MonRgLoad 
                                                       | where ClusterName == 'Dynamic_Source_Ring_Name' 
                                                       | where TIMESTAMP > ago(30m) 
                                                       | where event == ""instance_load"" and code_package_name == ""Code"" and service_manifest_name == ""SQL""
                                                       | summarize by application_name , slo_size , cpu_count
                                                       | where slo_size != ""PQUARTER""
                                                       | extend AppName = extract(""fabric:/Worker\\.(.+)/([a-z0-9]+)"", 2, application_name)
                                                    ) on AppName 
                                                    | join kind= leftouter (
                                                        MonRgLoad
                                                        | where TIMESTAMP > ago(1h)
                                                        | where event == ""reporting_win_fab_metric"" and metric_name == ""InstanceDiskSpaceUsed""
                                                        | where application_type == ""Worker.ISO.Premium"" or application_type == ""Worker.ISO""
                                                        | summarize (TIMESTAMP, DiskUsageMB) = arg_max(TIMESTAMP, metric_value) by application_name, NodeName
                                                        | summarize DiskUsageMB = sum(DiskUsageMB) by application_name
                                                        | order by DiskUsageMB desc
                                                        | project AppName = extract(""^.*/(.*)"", 1, application_name), DiskUsageMB
                                                    ) on AppName
                                                    | join kind= leftouter (
                                                       MonDmRealTimeResourceStats
                                                       | where TIMESTAMP > ago(2h) and replica_type == 1
                                                       | summarize max(avg_cpu_percent) , max(avg_log_write_percent) by AppName , server_name , database_name
                                                       | extend is_hot = max_avg_cpu_percent > 50 or max_avg_log_write_percent > 30
                                                       | summarize is_hot = max(is_hot) by server_name, AppName
                                                       | project AppName, server_name, is_hot
                                                    ) on server_name, AppName 
                                                    | join kind= leftanti (
                                                        MonFullTextInfo
                                                        | summarize by AppName, server_name = LogicalServerName
                                                        | extend is_fulltext = true
                                                    ) on server_name, AppName 
                                                    | join kind= leftouter (
                                                       MonBillingResourcePoolStatus
                                                        | where TIMESTAMP > ago(1h)
                                                        | summarize by pool_name = resource_pool_name, pool_id = resource_pool_id 
                                                    ) on pool_id 
                                                    | join kind = leftouter(
                                                        MonBillingDatabaseStatus2
                                                        | where TIMESTAMP > ago(2h) 
                                                        | summarize arg_max(TIMESTAMP, service_level_objective_id) by logical_database_name = database_name, server_name = billing_server_name
                                                    ) on logical_database_name, server_name
                                                    | project unsafe_sub, is_hot, sub_id, server_name , database_name = logical_database_name , slo , AppName , cpu_count , DiskUsageMB, pool_id, pool_name, service_level_objective_id
                                                    | extend service_level_objective_id = iff(service_level_objective_id != '', service_level_objective_id,
                                                        iff(slo == 'Basic', 'dd6d99bb-f193-4ec1-86f2-43d3bccbc49c',
                                                        iff(slo == 'System0', '29dd7459-4a7c-4e56-be22-f0adda49440d',
                                                        iff(slo == 'System1', 'c99ac918-dbea-463f-a475-16ec020fdc12',
                                                        iff(slo == 'System2', '620323bf-2879-4807-b30d-c2e6d7b3b3aa',
                                                        iff(slo == 'System2L', '53f7fa1b-b0d0-43d6-bc29-c5f059fb36e9',
                                                        iff(slo == 'System3', '33d0db1f-6893-4210-99f9-463fb9b496a4',
                                                        iff(slo == 'System3L', 'e79cd55c-689f-48d9-bffa-0dd12c772248', ''))))))))
                                                    | extend command = iff(isempty(pool_id),
                                                    strcat('Set-Database -ServerName ""', server_name, '"" -DatabaseName ""', database_name, '"" -ServiceObjective ', service_level_objective_id, ' -Settings ', ""'"", '{""RequestedTargetTenantRingName"":""', 'Dynamic_Target_Ring_Name', '""}', ""'""),
                                                    strcat('Move-ElasticPool -ServerName ""', server_name, '"" -ElasticPoolName ""', pool_name, '"" -RequestedTargetTenantRingName ""', 'Dynamic_Target_Ring_Name', '""'))
                                                    | project unsafe_sub, is_hot, server_name, database_name, AppName, cpu_count, DiskUsageMB, slo, command, pool_name, pool_id, sub_id
                                                    | summarize pool_db_count = countif(isnotempty(pool_name)), database_name = any(database_name) by AppName, cpu_count, DiskUsageMB, server_name, slo, pool_name, pool_id, command, sub_id, unsafe_sub, is_hot
                                                    | extend database_name = iif(isempty(pool_name), database_name, """") 
                                                    | project unsafe_sub, is_hot, slo, AppName, sub_id, cpu_count, DiskUsageGB = (DiskUsageMB+1023)/1024, command, server_name, database_name, pool_name,  pool_id, pool_db_count
                                                    | where AppName == 'Dynamic_AppName'
                                                    | order by unsafe_sub asc, is_hot asc, slo asc nulls last";

        #endregion
    }
}
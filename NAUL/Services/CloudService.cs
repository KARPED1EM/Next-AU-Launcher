using NAUL.Models;
using System.Collections.Generic;

namespace NAUL.Services;

internal class CloudService
{
    public static List<AssemblyInfoItem> RequestAssemblyMODInfo()
    {
        // Wait for compelete
        var list = new List<AssemblyInfoItem>()
        {
            // Steam
            new ("07b382a782ea344a5eae9f57d8ea4054", GamePlatforms.Steam, new("2023.11.28")), //28 November 2023 – 180341 UTC
            new ("076e8c8e0ec61642f4d276f23fc759a8", GamePlatforms.Steam, new("2023.10.24")), //24 October 2023 – 170026 UTC
            new ("4c159725b4872eda509dfecfef3d0293", GamePlatforms.Steam, new("2023.7.13")), //13 July 2023 – 233016 UTC
            new ("085252ebb7c7f180a7adaa0e9bb9b06c", GamePlatforms.Steam, new("2023.7.12")), //12 July 2023 – 202602 UTC
            new ("382daa321feb0d79ba98e5123cbeb4cc", GamePlatforms.Steam, new("2023.7.11")), //11 July 2023 – 170546 UTC
            new ("34fcb9efd459d85f4880f3b2f9d47931", GamePlatforms.Steam, new("2023.6.28")), //28 June 2023 – 203812 UTC
            new ("718d801c5049a6acf2a1cc132c48aed8", GamePlatforms.Steam, new("2023.6.13")), //13 June 2023 – 170133 UTC
            new ("1bf15d0a96942368f47fb453b4a8d037", GamePlatforms.Steam, new("2023.3.28")), //28 March 2023 – 170009 UTC
            new ("1a5c3653a738ca2e64c948aed2b19461", GamePlatforms.Steam, new("2023.2.28")), //28 February 2023 – 180247 UTC

            new ("3995dd575e6bd69dc53fea833cdebfc1", GamePlatforms.Steam, new("2022.12.16")), //16 December 2022 – 180128 UTC
            new ("0ae9bce0e94e05f91277f06638eae7b1", GamePlatforms.Steam, new("2022.12.9")), //9 December 2022 – 180218 UTC
            new ("87b9b80a8b790868574fb16bcdb78be0", GamePlatforms.Steam, new("2022.10.19")), //19 October 2022 – 192942 UTC
            new ("9e7397533ec14f02b5e845f37fb2998d", GamePlatforms.Steam, new("2022.10.18")), //18 October 2022 – 170320 UTC
            new ("bcab27163934f05a01525698e794d262", GamePlatforms.Steam, new("2022.9.20")), //20 September 2022 – 170403 UTC
            new ("e8ba94c81afee944eeddef3093000595", GamePlatforms.Steam, new("2022.8.24")), //24 August 2022 – 024922 UTC
            new ("9f6775a2b9cbb7ebeaa5df9c23bb82d5", GamePlatforms.Steam, new("2022.8.23")), //23 August 2022 – 170330 UTC
            new ("5a1402ee6de706ae286d4d0ec0fc3a16", GamePlatforms.Steam, new("2022.7.13")), //13 July 2022 – 185902 UTC
            new ("cd7a43d29af18b9a966780bad2b5cebe", GamePlatforms.Steam, new("2022.6.21")), //21 June 2022 – 165956 UTC
            new ("26b804e6f6e87851b6a96db9de2add35", GamePlatforms.Steam, new("2022.3.31")), //31 March 2022 – 165941 UTC
            new ("67e0db0f8545de0c86524dcf16c70a47", GamePlatforms.Steam, new("2022.2.25")), //25 February 2022 – 015249 UTC
            new ("f4a526b7e6142be5004482e5af0d31fe", GamePlatforms.Steam, new("2022.2.24")), //24 February 2022 – 210912 UTC
            new ("1ea13e992ab6038c0948f328c0386e83", GamePlatforms.Steam, new("2022.2.16")), //16 February 2022 – 220310 UTC
            new ("e60db8d145c56fc7b143db47f4ab5973", GamePlatforms.Steam, new("2022.2.3")), //3 February 2022 – 001941 UTC

            new ("5dcd2f24eb4947c0f30cbac531f94aa8", GamePlatforms.Steam, new("2021.12.17")), //17 December 2021 – 011925 UTC
            new ("0018ab80475aca2a611aa7c8caa8b185", GamePlatforms.Steam, new("2021.12.16")), //16 December 2021 – 203421 UTC
            new ("7b1efdd7060b1aa06dd88b1bb0bdd95f", GamePlatforms.Steam, new("2021.12.15")), //15 December 2021 – 004429 UTC
            new ("f306fc1b456084529ab429bc31bcc039", GamePlatforms.Steam, new("2021.12.14")), //14 December 2021 – 185950 UTC
            new ("311d201bc283d48b0e54e7384a66babe", GamePlatforms.Steam, new("2021.11.11")), //11 November 2021 – 213623 UTC
            new ("89f26c2d05640af8283985b4fc2a07ac", GamePlatforms.Steam, new("2021.11.10")), //10 November 2021 – 202638 UTC
            new ("fb9175dffb3558bcba23ef289c333e52", GamePlatforms.Steam, new("2021.11.9")), //9 November 2021 – 190016 UTC
            new ("b796dd913f7739ce751b6631079fd229", GamePlatforms.Steam, new("2021.7.7")), //7 July 2021 – 160146 UTC
            new ("73da8090807f65ea9fceedfc86dc631d", GamePlatforms.Steam, new("2021.6.15")), //15 June 2021 – 190042 UTC
            new ("317ea83008f3aad94d50efeb328ac9ac", GamePlatforms.Steam, new("2021.5.27")), //27 May 2021 – 222923 UTC
            new ("328895f9f4e9b9328eb076e8278f2536", GamePlatforms.Steam, new("2021.5.26")), //26 May 2021 – 213706 UTC
            new ("f89a11fe67e02476402bcf98df9cc453", GamePlatforms.Steam, new("2021.5.10")), //10 May 2021 – 200137 UTC
            new ("746beecf089e6a273a3748592b2b97a1", GamePlatforms.Steam, new("2021.4.14")), //14 April 2021 – 212609 UTC
            new ("ba010639abead22a79a27626ef95832f", GamePlatforms.Steam, new("2021.4.14")), //14 April 2021 – 182512 UTC
            new ("741cba003ef99cd846d63beadf49dc69", GamePlatforms.Steam, new("2021.4.14")), //14 April 2021 – 131944 UTC
            new ("f09d1c48cc3ca7f5ae0658b1067ead17", GamePlatforms.Steam, new("2021.4.13")), //13 April 2021 – 190024 UTC
            new ("1171dc4a8c300767b13fb1c8da621e64", GamePlatforms.Steam, new("2021.4.12")), //12 April 2021 – 210338 UTC
            new ("840ced82d7e20a35409f1abc075b8cf1", GamePlatforms.Steam, new("2021.4.2")), //2 April 2021 – 001759 UTC
            new ("b9f676768ff4fc2dd173f40a8c1df87a", GamePlatforms.Steam, new("2021.4.1")), //1 April 2021 – 035305 UTC
            new ("223bcb0b78e0c502096fc3ccdbfb1b1f", GamePlatforms.Steam, new("2021.3.31")), //31 March 2021 – 215924 UTC
            new ("5c40809dbd45c469bfae717e872ebec9", GamePlatforms.Steam, new("2021.3.31")), //31 March 2021 – 180152 UTC
            new ("39244737f0f583484b8a281e4149b66e", GamePlatforms.Steam, new("2021.3.5")), //5 March 2021 – 214935 UTC
            new ("7937d3cb8bcc9fab22d6c96242cf00b5", GamePlatforms.Steam, new("2021.3.5")), //5 March 2021 – 214134 UTC
            new ("2b0a6f1995db29f0d5b4e8e0daf59d70", GamePlatforms.Steam, new("2021.3.5")), //5 March 2021 – 202217 UTC
            new ("746e84e41123dbc9460093f0dd185f61", GamePlatforms.Steam, new("2021.3.5")), //5 March 2021 – 191554 UTC

            new ("ebf9a16fdaf2ba4f3b9fd0d270101c8f", GamePlatforms.Steam, new("2020.12.10")), //10 December 2020 – 224335 UTC
            new ("849f4fa24a95d5e55ad487b0afe131bc", GamePlatforms.Steam, new("2020.12.5")), //5 December 2020 – 205953 UTC
            new ("12f1cf6634d8a0668a05c638eba78a29", GamePlatforms.Steam, new("2020.12.4")), //4 December 2020 – 183816 UTC
            new ("463585f0d1a4071ae006ce8b515d8888", GamePlatforms.Steam, new("2020.11.24")), //24 November 2020 – 183230 UTC
            new ("694eb5fc543c951ebe5767ad148bd16a", GamePlatforms.Steam, new("2020.11.23")), //23 November 2020 – 235453 UTC
            new ("c55eb78376e7d801a18cf3736a1caa68", GamePlatforms.Steam, new("2020.11.6")), //6 November 2020 – 073006 UTC
            new ("df50b2173ee21e6d2266a45a942329e0", GamePlatforms.Steam, new("2020.11.3")), //3 November 2020 – 010804 UTC
            new ("400d86ab5f40ebca23fe08540cce384f", GamePlatforms.Steam, new("2020.10.9")), //9 October 2020 – 170258 UTC
            new ("b50ee1b115e5f50d15601897223a969b", GamePlatforms.Steam, new("2020.9.24")), //24 September 2020 – 210217 UTC
            new ("8268908c1dc24acc721d32699e9d7a37", GamePlatforms.Steam, new("2020.9.23")), //23 September 2020 – 025238 UTC
            new ("c03beae37c707c9b2e9c44106b7b03b4", GamePlatforms.Steam, new("2020.9.19")), //19 September 2020 – 013454 UTC
            new ("952822a7eb97786852a50f120530d9b3", GamePlatforms.Steam, new("2020.9.18")), //18 September 2020 – 235625 UTC
            new ("66535840cee073de35ddfe93e07af704", GamePlatforms.Steam, new("2020.9.14")), //14 September 2020 – 154125 UTC
            new ("babba191aeba23c08eb1966f7bbbb41c", GamePlatforms.Steam, new("2020.9.10")), //10 September 2020 – 220133 UTC
            new ("7a7ac8d165e640f74a1b142b79b55492", GamePlatforms.Steam, new("2020.9.2")), //2 September 2020 – 030402 UTC
            new ("6b65f0e61267820c4f1afad90a5424d5", GamePlatforms.Steam, new("2020.9.2")), //2 September 2020 – 030347 UTC
            new ("fec5d8308e57bc788db0341dc56a905c", GamePlatforms.Steam, new("2020.9.1")), //1 September 2020 – 073800 UTC
            new ("4164610040ebd599d0d0a6b8bd298f4d", GamePlatforms.Steam, new("2020.9.1")), //1 September 2020 – 013132 UTC
            new ("ca5257dd1dd2c2789cb56cf18ea41aa1", GamePlatforms.Steam, new("2020.8.29")), //29 August 2020 – 182059 UTC
            new ("2e6c4601617bbb957e2a9fed21348d0d", GamePlatforms.Steam, new("2020.8.29")), //29 August 2020 – 170150 UTC
            new ("b107a59bf413de93f61a65b2844fb1d1", GamePlatforms.Steam, new("2020.8.29")), //29 August 2020 – 013534 UTC
            new ("b5617de09e7b4dc838f91414ab682baa", GamePlatforms.Steam, new("2020.8.18")), //18 August 2020 – 231441 UTC
            new ("0a4feecb632181218de2a99f260d6de1", GamePlatforms.Steam, new("2020.8.12")), //12 August 2020 – 183712 UTC
            new ("9b570139f539e9a090366a3080ab9ec4", GamePlatforms.Steam, new("2020.7.24")), //24 July 2020 – 033749 UTC
            new ("c7d05a051b06f03f5f1fa81dabeebb1b", GamePlatforms.Steam, new("2020.7.6")), //6 July 2020 – 235449 UTC
            new ("a9c2f36b06cce66056bee7a3e6c60f8c", GamePlatforms.Steam, new("2020.6.11")), //11 June 2020 – 200317 UTC
            new ("4f88f73baae4cad88c90fa0ba2e8f254", GamePlatforms.Steam, new("2020.6.11")), //11 June 2020 – 175008 UTC
            new ("8523382badfad828751db0533a993a42", GamePlatforms.Steam, new("2020.5.10")), //10 May 2020 – 191159 UTC
            new ("bfa34005e4d4b6899c2942cad674e342", GamePlatforms.Steam, new("2020.5.8")), //8 May 2020 – 235554 UTC
            new ("aaf8eb3866f6ec2dafbccf8064a88c28", GamePlatforms.Steam, new("2020.5.8")), //8 May 2020 – 223505 UTC
            new ("68a047dab659c01bedd524c46a1d2c99", GamePlatforms.Steam, new("2020.5.1")), //1 May 2020 – 232458 UTC
            new ("340b4a946615d8c34d49de7002383b33", GamePlatforms.Steam, new("2020.4.16")), //16 April 2020 – 182018 UTC
            new ("d9b1d758d6248881e189301aa18bf023", GamePlatforms.Steam, new("2020.4.4")), //4 April 2020 – 184229 UTC
            new ("bf4eab401391896e92b49df1d52514df", GamePlatforms.Steam, new("2020.3.30")), //30 March 2020 – 195543 UTC
            new ("ca629ec6818349ec690e18741a0cc15b", GamePlatforms.Steam, new("2020.3.28")), //28 March 2020 – 034817 UTC
            new ("59e7557c5fb16cc2982466bfc39b4a02", GamePlatforms.Steam, new("2020.3.26")), //26 March 2020 – 232403 UTC
            new ("2c7edd9008ee6fb6a24273e7e6daa74d", GamePlatforms.Steam, new("2020.3.26")), //26 March 2020 – 221424 UTC
            new ("33d0c191a3a79fa27f981f56ad2712c7", GamePlatforms.Steam, new("2020.3.25")), //25 March 2020 – 235654 UTC
            new ("737385222beb76ec3f7d6c66e6c1e68a", GamePlatforms.Steam, new("2020.2.18")), //18 February 2020 – 052952 UTC
            new ("9f60f774b470909da704b541bee2bdd3", GamePlatforms.Steam, new("2020.2.18")), //18 February 2020 – 005233 UTC
            new ("e830461b6985282b495282cbab1004b6", GamePlatforms.Steam, new("2020.2.10")), //10 February 2020 – 223859 UTC
            new ("8fb6013aefcbe9e728ab86b8390fa6f5", GamePlatforms.Steam, new("2020.2.4")), //4 February 2020 – 053203 UTC
            new ("c9b678a6d086834a02291b0ff51cd39f", GamePlatforms.Steam, new("2020.1.17")), //17 January 2020 – 232123 UTC
            new ("68d60628abc047c559d681411b92fb05", GamePlatforms.Steam, new("2020.1.14")), //14 January 2020 – 220345 UTC
            new ("d3cc6b8b921ad5b90b652dedfd4a2891", GamePlatforms.Steam, new("2020.1.7")), //7 January 2020 – 234519 UTC
            new ("c4f7a9715db4bb4962b38d91b9031a6b", GamePlatforms.Steam, new("2020.1.7")), //7 January 2020 – 233119 UTC

            new ("7434b79c82ddb0977e3ca91023a70698", GamePlatforms.Steam, new("2019.12.18")), //18 December 2019 – 000725 UTC
            new ("58c122230fec73bdce7ea7fe470911e1", GamePlatforms.Steam, new("2019.12.17")), //17 December 2019 – 020125 UTC
            new ("7c2d7aa6c10d482cda5c781ea07bfcd5", GamePlatforms.Steam, new("2019.11.12")), //12 November 2019 – 175428 UTC
            new ("f494989fc4a0385038b43e97cb700e7b", GamePlatforms.Steam, new("2019.11.5")), //5 November 2019 – 222314 UTC
            new ("4456b97b5d44062b7d659f6c1c83409b", GamePlatforms.Steam, new("2019.11.5")), //5 November 2019 – 212452 UTC

            // Epic
            new ("2c504162c16af930a7176361a3558d71", GamePlatforms.Epic, new("2023.11.28")),
            new ("8a3ae7e799e506aea5de1e72a846c87d", GamePlatforms.Epic, new("2023.10.24")),
            new ("499bf5c2fc6aeb335e380f8156b6569d", GamePlatforms.Epic, new("2023.7.12"))
        };
        return list;
    }

    public static List<PluginInfoItem> RequestPluginInfos()
    {
        // Wait for compelete
        var list = new List<PluginInfoItem>()
        {
            new()
            {
                PluginName = "TONX",
                PluginType = PluginTypes.Single,
                IconUrl = null,
                Author = "KARPED1EM",
                URL = "https://tonx.cc",
                License = "GPL-3.0",
                Description = "A TOH branch mod",
            }
        };
        return list;
    }
}

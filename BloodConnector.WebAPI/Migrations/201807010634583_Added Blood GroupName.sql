﻿ALTER TABLE [dbo].[BloodGroup] ADD [GroupName] [nvarchar](32)
ALTER TABLE [dbo].[BloodGroup] ALTER COLUMN [Symbole] [nvarchar](16) NULL
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201807010634583_Added Blood GroupName', N'BloodConnector.WebAPI.Migrations.Configuration',  0x1F8B0800000000000400ED5D596F23B9117E0F90FFD0D0530EAFE46366B019C8BBF0F8D835E20B237B93B701A5A6E5C6F4A1EDA6BC7682FCB23CE427E52F84EC9377937DA9BD59F8C5E2512C92C562B158FDF1BFFFFECFFCFB97C0779E619C7851783C3998EE4F1C18AE22D70BD7C7932D7AFCE6DBC9F7DFFDFE77F3733778717E2ACA1D9172B866981C4F9E10DA7C9CCD92D5130C40320DBC551C25D1239AAEA26006DC6876B8BFFF97D9C1C10C6212134CCB71E69FB721F20298FEC03F4FA3700537680BFCEBC8857E92A7E39C454AD5B901014C3660058F279FFC2872718510AE50144FFF0697277797D3ACDEC439F13D80795A40FF71E280308C104098E38F0F095CA0380AD78B0D4E00FEFDEB06E2728FC04F60DE938F5571D34EED1F924ECDAA8A05A9D53641516049F0E0281FA5195FBDD1584FCA51C4E3788EC71BBD925EA763793C394108AC9E0218A289C337F7F1D48F4951FD604F2B127B8EB4E05E293058AEC8DF9E73BAF5D13686C721DCA218F87BCEDD76E97BABBFC2D7FBE82B0C8FC3ADEFD38C63D6711E938093EEE2680363F4FA193EE6DDB93C9B3833B6DE8CAF5856A3EA64DDBC0CD1877713E706370E963E2CE5821A9205EE16FC0186300608BA77002118E369BD74613AB242EB5C5B58FEE24BB7680F8B225E5F13E71ABC5CC1708D9EF0CA3BFC76E25C782FD02D52721E1E420F2F475C09C55B28E151DFEE85E7C3F556DBF2E1FB0F462DEB1B22E4A9E13C3A6CC42AF9AF7756B198223C6B34C72D2644DFD6C2FB07AC93323D852B90A0878D4BA4EE362C489DE19FF75E60200F37E0D95BA7222C91C889F319FA6966F2E46D32B5492DEB2F59998B380A3E473EA334D2AC2F8B681BAF48FF2279FE3D88D710B13CCD67952ED26AA8ACF186BA89541E8156721B68A51D6809563BF5AF0D2FBC384183AC75B27A0669E8C6FB3A8CF64A65FA8738DA6E98196BA070334ADE330CDBD139F1C9EC63C1788644B58215BA897A57AC4403DE3E7EF262F4242AC51A7E5D378649A26111FFDB018B7751824E71E9DEC7E2345D727D3712619B3D7E1584AE6EFDA1AB88F0A3E4EEFD7E07CC6155E492DD226F856C635952B6C9EB2BE33DD05BA7563655BD4AAC9964BC974421F00BD577B30DFA9F8A1812B54BE4DDDE20E0557F665A7442EB3C009EAF9BEA83C30E7A9FB68215CDA31707B094C64F11B6404068CDF31D48925FA2D8FD11244FBD6B84055C6D632C240B04824DFFFAE7290A2196C725B5320668ABB3A9B9FF25BA00C4703B0F49ADD6F4AEA2D5D7688BCEC354DA1FD0CA76E3280974C2CEC96A8577A10B2CCCD04D956BBB5D98D8701D19208DCF1495FD9FB43D5A144707D5D1A2387A98B256994D52CE529A74998A332E4B38F4F0F9B2438F8EB3531F78817CBC52D2453EC751962CE726CFB3E624DBE235AC1405785EB274053379A62D3757D1DA0B35E352E473BC64C97256F23C5B4E08050D237936C7479A2A6723CB6A7538A645B5E111B922318283725BF79D5461D61F586FF0AE55BB69BF064B3C613AABAE8B735D3A13350AFCA891FDA4750169E4DA4C1DF28A5AA52E1B8979615513A2A952D349FB75E90D3F493637104D8BDAD38CEE458C69625BEFEB5420BBE71857AE56C5A1E9AA383A583E1E7DFBFE03708F3EBC8347EF877725355F21637770A7D357E3CAEDC6C44D5BFA09F8DBAE9B325E0DE5F6DB50E3E7F57F53F75A479E5E05777382BF8F7EB98284A91AFF5033852F3B9BDDC5F0D17BB13C6234DD39EAEC44E99EC11B91AD378CD4DAEB7EC348C98E7FC348D9C4C9CF5EEA199BD5D7280A63F246E50B956FBB9239CE86DE31986EEEF6A6A5AF761B2D97CFA991DBF56A2154C7BF58E4A22C2D4A3AD444EA77652015FC8E44E252D3E33E0661025659684B9BE32B45E85760D5F47CEB7A168551372258779FB282DE7347D25E7F0BD8614C443A4252B38697B62F79D1CAC09197101C408A62D62EA97C8CCD98AD4A6BF82D0AD5B35C966CE5C22AC4B6FB7DE76DEC396F253864D7970AEAB3063DDF5FF26295808BB982644B8AD889F436A0049ABE71BE4C2E7CB0AE224C6D37B907E4F91EF26032258D247FAE6877BCCFE169C284FD573CADF4D1909D9B6B482EF68AE01D1800B2625357C8F1645F9849A6F4355DF6401CD76C0435A35A5DC4773DA605E59D8FE865E2834035487CE1D3A7D84B9007C274C7CFEB8871184C9D1FBDD0DD7A49D5C691BEFCA7ADEB3ED1E5DFE9CBDFA227B2B9E485DF9BCCF24992442B2F9D376A9DD33E67B6BDF3D0756AEEE3AAE08DEC72F11ACF96B7C1F38347EA78F227A10B6A92A57FA222493BC359C207135ED7DF8667D087083A27AB2CF8FB14242BE08A3A0F8F8CCBA6E0ED01C644290112E2906029F34224EE255EB8F236C0D7B3CF5533B459095B65037CCE19DC10451422FD5C98B4CCC6B1893C944D7143563742F319255A0612975FAF6A4583BF6BD54B5AA70221E74422A092AB92BA05D05CD8D801311234E924DB091ADB79935695E7FAE1C42B7785EA6795BF3F6FA9CA043F6B45AF74CDB224F7A75371DF69211F2C034369226E1C4D9AA5C21A772724792883764EF9B886DDE9202E8A42A18372EF7B7F3A881D90A17410DBF9B7A183B20015ED9C72D12ABB132E363646215B99CBA03FD162466328C9627A3E7AC1E243F95493AAFC64A89A59FA5344F38D4E193038A0EC36923005E3038899622E462F6B0AE7AB4A32EA3CB1DCA18EB98C3097BF1A476E8D1876676FE9F91840AAF4C36DC2407931310EE92ABDE5C6532FBACE7B9231C1F3BE3B31E359D985A4F1E36EC2037D3BB513799378AD55F3AF73618B9691CC2AEAC954D338CE8737D9D4A334804CAA47C24818A5C104BD08627685917EA0E985A5E7F6644366232D7CB64C3F8C7F910142E08EE5CEF524BFCFE12583105F4024588713A7BA3B91D87B82EA63E9E4973A02894CD7D554A65DB602053AB3868EE05993712471BFD5902D3D3102AD32C782B1E26B082D63F999DC826CFE6D83966AB6B44D2683D906E553C214A9A159C79C94316A79B072C6C4BB53A51421F1FC8AADBFA028D92FE55A58F6F5571214118D08CFD85E9A8E4021DD8ADECB5CE5B5CE72FB5E733E6E8A806C2576D4F362C9A9BA2E73E3D63B721B749E73DD5214546AA161970B8DA1E8B1CC2759EB95B4EF2FE74C544C76C16B373DCFF586A2E3126F599DBFCCBEDBAC9B4BD1EB9CCFD69D163EBB147BAE75E7183974A83EA83778230F4EDD6836180155F896381026BE061B6F03AFAF35FB9B8D7B618831AAA2C60C86497E68B63C36773658C239B987F192451F89035577DA333DEF511D90AA05D3E35947AAA608892ACF1365DE7C9661F6E509F39902DC6F7E0D361B2F5C53607F798AB3C890FE4EBF59D803DF05198DD98A1967FEF453B684A218AC21974BB65A17A620456700812520D146A76E2014939E9E14366BD1A4704012A7B130678B2AE47F6A8DD6E3F149CE9E39AD0BDC5D5222ED3994EB6CA1B2437018810F6249F0F269E46F83507D85ABAE5DB894690A2A37B39A4A057047D3A952CD2965817B341519784C1D2F59B424CF4B966A4E8981A8A3893119E6F432183A9A5096624E8183A1A349715922CDF98C933EC1DB21C8BCE08A625791D11A5369DB86CB4B6617192C2C7935E592E204D94E84BB5A52253A1B2BC765B29DD888A4AA54734A15C01A4DA94A35A7C4469ED1D4F431697514332435915E966E4E4D8EA746D39597306F81014FA3093319161C17886A0C9345A2399D0A318D2654A55A28D0343A96D19C922F64B414AAC02056012BE385D4B44A14347611E489E6740A98339A4C91664EA58AA8A6E9A800CFB4F325033F63A64E56C0620E68A0336616E80C0BFD48839D314A92CE30A797039ED194F2244B1A146696408CCAB3981906D68C991226C7C28460B1CB185B82CDB2E09246286398A4331AD1538CA8BC8485912860923106A3906BA123447432465B88D90D684B78E6F32CB4BE0860C6E87F31DBCE921177FB2A753446A6F646A0A1A5A9A669606FEA2AF773902B0192189D50245AEC6E150212B3C155C9A39975C9454467B32FDE24DA0B81018D319F4028501F66D7AF922D69E5B03D02B13C7D3472A5BE666A284C0A820622A4ACD98F12911CED2C8F751CA60EB32FB359960645019D23D81145C668C4477265D68B5ACAE208DAA925050DB509C380C6B0168C16E94633BB34120C33BB3A241C353D3BE5370219515D7576202269EC453B099193E877E72902B29893B122486BB736277D37D6ADE5A9A16C6A7F6A49F4B3819441C48C3F4B1559AC91002A3E94F58FA8E346355C09AE06958F6147B2D4930A68B9FCED967E3B93D5DCF2E8758A841B55BE48D97A79B3CADDA0CEF3DBCCFA37D484EBCDACC8C429363E727E4B100CA6A4C074F1B37FEA7B69384751E01A84DE234C50867B3039DC27D0FECCE36BE379086D9624AE2FB90DA6402654B79F43003F2D3D6CBEE0A1AD45776A0535173E8378F504642F2A5D862E7C399EFC33ADF7D1B9FCFB97ACEA9E731BE3A9FEE8EC3BFF1281665A3E4456F1C363C954846DDF1D4B87B1E5AB635DF12579644C4A9A1B4BDB37C50AE1E9E25131F213D50368D93DE0D56439B502463219E506EBA718E8BAD5F320AC9E833DE7327908BD9FB738FB1ECF2F594D5D2F76E145ADAEE4987F40AB2BBAFC7B595DD1953D8F65347158EDD17559E5D7FA65AD268B54F7AE5657BA44F28C964C0D9890E29ED52A38FC43005EFE684B8B7F3AABABEED22F657546937F18CB54DCCA8A2A59335B9FCC035BD24E9127B6ECA8B2EF6971C26B8601C9BEA9D58084F4EEB8E3A9131FD232DC0425DB85F89056635ACC435AF2293D38B4EDACFCDDACA567AF98646F66B55AEED277B1DA2910F1EDABAEE8753284AAB7AD9AD052BE6BD55493CBDFB96AC29AF28DAB26FB6175FB6B7BA4296AA6C932734C6D73B4C63A9681D90D70AA6C32C0DC4B37722D6B6D91090FDB48E91ED5E8B3164FC60C7E04F93F38CE0BCFAEB4D2AEE2D32A16E4AC9F4F79136BB176C1343077A4AF8E345A8D8A0D927D64A4BEDB2D9EF56830896FEC458CCE1C1977E283173D394986D54ACD5FB968203C4A0024B168372F44EC5AC3B3EF46587193551D6266F5978D6FD97BCF3DD66035FE795DD30968F6A6839D4494D5BB64AAC159BBD9EB046FD789DDECBC443ADDFF79A93904388D7CA1C074190CB1B2015A6F73002E5550B08956736C11297F6D30DEBBC235954003E981837A03361D12104B17BE6C615E8D06A8BB445F91013519A0EEED466BA8E37F77A732DE0AE4F618F4458ED8A5C7F3FAD5E80B555CF118F5453DA8F618E4E733E5CF1D1A467B68E951C41C8E4C784C81B3772941348013C5810D50F7A8A5468B94323279B104BF36129B1A0345C45EE20F595678C6A31605838877B1CDB70658FD9B500C20146F15587A2C58D2BB34576A3E5B1887D9B273B06811158D9FD3FCEB0631AA5E8F029D7D7E703C719724C03173245AA2442B41A2A5B4D30761E540803C7163146959436D50A6CD40A6F5DD5380E4F26D1B4051CBDA698C536D0653ADEF9A021256D776A640B44D6745F42DCBB112A572630B78AD94211B586C56872BBB5BD7557937FB87CF16175C0DDAB5C4D4A1EA6509FC7EB43B7CEC5AE6EA7CC792CFE5A8DC8E3ADA1A0E9B53293A00EBE1E6AF0BC8EBE6F3C7A83EC5F7F01D75B43DC275F36ED26A47FE4D77FB4E768A686DDF5DD1B0A9C1BD1E1764B57D87559B991154F318B1A8C73F043DC34B4B57A86C750EB2CE2DE0A4C58F9CF13907EF2B5E900517E0B356E2AD2B12F3FC5375FA845396B90C1FA3E2A0C5715414E1022AAE21022E3EFE9CC4C87BC4938DB34988BB473EC1C9DF3F3F0F96D0BD0C6FB768B345B8CB3058FACCA6470E6CBAF653CC6C96E7F9ED86FC4ABAE80266D323E132B7E1A7ADE7BB25DF1792D00D05097212CC839DC85C2212F4B4AE9EA3BF89424342F9F09507D87B186C7C4C2CB90D17E01936E10D8BDF155C83D56B1550A922523F11ECB0CFCF3CB08E4190E434AAFAF82796613778F9EE7F9E25A9BF55AD0000 , N'6.1.2-31219')

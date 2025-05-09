µ;
P/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/UserService.cs
	namespace		 	
src		
 
.		 
Services		 
;		 
public 
class 
UserService 
: 
IUserService '
{ 
private 	
readonly
 
IUserRepository "
_userRepository# 2
;2 3
public 
UserService	 
( 
IUserRepository $
userRepository% 3
)3 4
{ 
_userRepository 
= 
userRepository $
;$ %
} 
public 
List	 
< 
UserDto 
> 
GetAllUsers "
(" #
)# $
{ 
return 

_userRepository 
. 
GetAllUsers &
(& '
)' (
;( )
} 
public 
bool	 

CreateUser 
( 

UserCreate #
user$ (
)( )
{ 
return 

_userRepository 
. 

CreateUser %
(% &
user& *
)* +
;+ ,
} 
public 
async	 
Task 
< 
UserDto 
> 
Authenticate )
() *
UserCredentials* 9
userCredentials: I
)I J
{ 
var 
user 
= 
await 
_userRepository $
.$ %
GetUserByEmailAsync% 8
(8 9
userCredentials9 H
.H I
	UserEmailI R
)R S
.S T
ConfigureAwaitT b
(b c
falsec h
)h i
;i j
if 
( 
user 
== 
null 
) 
{   
return!! 
null!! 
;!! 
}"" 
var## 
passwordHasher## 
=## 
new##  
PasswordHasherHelper## 1
(##1 2
)##2 3
;##3 4
var$$ 
verificationResult$$ 
=$$ 
passwordHasher$$ +
.$$+ , 
VerifyHashedPassword$$, @
($$@ A
user$$A E
.$$E F
PasswordHash$$F R
,$$R S
userCredentials$$T c
.$$c d
Password$$d l
)$$l m
;$$m n
if%% 
(%% 
verificationResult%% 
==%% &
PasswordVerificationResult%% 8
.%%8 9
Failed%%9 ?
)%%? @
{&& 
return'' 
null'' 
;'' 
}(( 
return)) 

user)) 
;)) 
}** 
public,, 
async,,	 
Task,, 
<,, 
bool,, 
>,, 

DeleteUser,, $
(,,$ %
UserCredentials,,% 4
userCredentials,,5 D
),,D E
{-- 
var.. 
user.. 
=.. 
await.. 
_userRepository.. $
...$ %
GetUserByEmailAsync..% 8
(..8 9
userCredentials..9 H
...H I
	UserEmail..I R
)..R S
...S T
ConfigureAwait..T b
(..b c
false..c h
)..h i
;..i j
if// 
(// 
user// 
==// 
null// 
)// 
{00 
return11 
false11 
;11 
}22 
var33 
passwordHasher33 
=33 
new33  
PasswordHasherHelper33 1
(331 2
)332 3
;333 4
var44 
verificationResult44 
=44 
passwordHasher44 +
.44+ , 
VerifyHashedPassword44, @
(44@ A
user44A E
.44E F
PasswordHash44F R
,44R S
userCredentials44T c
.44c d
Password44d l
)44l m
;44m n
if55 
(55 
verificationResult55 
==55 &
PasswordVerificationResult55 8
.558 9
Failed559 ?
&&55@ B
!55C D
string55D J
.55J K
IsNullOrEmpty55K X
(55X Y
user55Y ]
.55] ^
PasswordHash55^ j
)55j k
&&55l n
!55o p
user55p t
.55t u
Email55u z
.55z {
Contains	55{ É
(
55É Ñ
$str
55Ñ ê
)
55ê ë
)
55ë í
{66 
return77 
false77 
;77 
}88 
return99 

await99 
_userRepository99  
.99  !

DeleteUser99! +
(99+ ,
user99, 0
.990 1
UserId991 7
)997 8
.998 9
ConfigureAwait999 G
(99G H
false99H M
)99M N
;99N O
}:: 
public;; 
async;;	 
Task;; 
<;; 
bool;; 
>;; 

ModifyUser;; $
(;;$ %
UserDto;;% ,

userModify;;- 7
);;7 8
{<< 
return== 

await== 
_userRepository==  
.==  !

ModifyUser==! +
(==+ ,

userModify==, 6
)==6 7
.==7 8
ConfigureAwait==8 F
(==F G
false==G L
)==L M
;==M N
}>> 
public@@ 
async@@	 
Task@@ 
<@@ 
UserDto@@ 
>@@  
LoginWithGoogleAsync@@ 1
(@@1 2
LoginGoogleDto@@2 @
dto@@A D
)@@D E
{AA 
varBB 
existingUserBB 
=BB 
awaitBB 
_userRepositoryBB ,
.BB, -
GetUserByEmailAsyncBB- @
(BB@ A
dtoBBA D
.BBD E
EmailBBE J
)BBJ K
.BBK L
ConfigureAwaitBBL Z
(BBZ [
falseBB[ `
)BB` a
;BBa b
ifCC 
(CC 
existingUserCC 
!=CC 
nullCC 
)CC 
{DD 
returnEE 
existingUserEE 
;EE 
}FF 
varGG 
createdGG 
=GG 
awaitGG 
_userRepositoryGG '
.GG' (!
CreateGoogleUserAsyncGG( =
(GG= >
dtoGG> A
.GGA B
UsernameGGB J
,GGJ K
dtoGGL O
.GGO P
EmailGGP U
)GGU V
.GGV W
ConfigureAwaitGGW e
(GGe f
falseGGf k
)GGk l
;GGl m
ifII 
(II 
!II 	
createdII	 
)II 
{JJ 
throwKK 
newKK 
	ExceptionKK 
(KK 
$strKK =
)KK= >
;KK> ?
}LL 
varOO 
newUserOO 
=OO 
awaitOO 
_userRepositoryOO '
.OO' (
GetUserByEmailAsyncOO( ;
(OO; <
dtoOO< ?
.OO? @
EmailOO@ E
)OOE F
.OOF G
ConfigureAwaitOOG U
(OOU V
falseOOV [
)OO[ \
;OO\ ]
ifPP 
(PP 
newUserPP 
==PP 
nullPP 
)PP 
{QQ 
throwRR 
newRR 
	ExceptionRR 
(RR 
$strRR C
)RRC D
;RRD E
}SS 
returnUU 

newUserUU 
;UU 
}VV 
}XX €H
U/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/UbicationService.cs
	namespace		 	
Services		
 
;		 
public 
class 
UbicationService 
: 
IUbicationService  1
{ 
private 	
readonly
  
IUbicationRepository ' 
_ubicationRepository( <
;< =
private 	
readonly
 
IUserRepository "
_userRepository# 2
;2 3
private 	
readonly
 $
IBicingStationRepository +$
_bicingStationRepository, D
;D E
private 	
readonly
 '
IChargingStationsRepository .'
_chargingStationsRepository/ J
;J K
private 	
readonly
 "
IStateBicingRepository )"
_stateBicingRepository* @
;@ A
private 	
readonly
 
ITmbService 
_tmbService *
;* +
public 
UbicationService	 
( 
IUserRepository )
userRepository* 8
,8 9 
IUbicationRepository: N
ubicationRepositoryO b
,b c$
IBicingStationRepository 2#
bicingStationRepository3 J
,J K'
IChargingStationsRepository 5&
chargingStationsRepository6 P
,P Q"
IStateBicingRepository 0!
stateBicingRepository1 F
,F G
ITmbService %

tmbService& 0
)0 1
{ $
_bicingStationRepository 
= #
bicingStationRepository 6
;6 7
_userRepository 
= 
userRepository $
;$ % 
_ubicationRepository 
= 
ubicationRepository .
;. /"
_stateBicingRepository 
= !
stateBicingRepository 2
;2 3'
_chargingStationsRepository 
=  !&
chargingStationsRepository" <
;< =
_tmbService   
=   

tmbService   
;   
}!! 
public## 
async##	 
Task## 
<## 
List## 
<## 
SavedUbicationDto## *
>##* +
>##+ ,&
GetUbicationsByUserIdAsync##- G
(##G H
string##H N
	userEmail##O X
)##X Y
{$$ 
var%% 
savedUbications%% 
=%% 
await%%  
_ubicationRepository%%  4
.%%4 5&
GetUbicationsByUserIdAsync%%5 O
(%%O P
	userEmail%%P Y
)%%Y Z
.%%Z [
ConfigureAwait%%[ i
(%%i j
false%%j o
)%%o p
;%%p q
return&& 

savedUbications&& 
;&& 
}'' 
public(( 
async((	 
Task(( 
<(( 
bool(( 
>(( 
SaveUbicationAsync(( ,
(((, -
SavedUbicationDto((- >
savedUbication((? M
)((M N
{)) 
var** 
user** 
=** 
await** 
_userRepository** $
.**$ %
GetUserByUsername**% 6
(**6 7
savedUbication**7 E
.**E F
	UserEmail**F O
)**O P
.**P Q
ConfigureAwait**Q _
(**_ `
false**` e
)**e f
;**f g
if++ 
(++ 
user++ 
==++ 
null++ 
)++ 
{,, 
return-- 
false-- 
;-- 
}.. 
var// 
result// 
=// 
await//  
_ubicationRepository// +
.//+ ,
SaveUbicationAsync//, >
(//> ?
savedUbication//? M
)//M N
.//N O
ConfigureAwait//O ]
(//] ^
false//^ c
)//c d
;//d e
return00 

result00 
;00 
}11 
public33 
async33	 
Task33 
<33 
bool33 
>33 
DeleteUbication33 )
(33) *
UbicationInfoDto33* :
ubicationDelete33; J
)33J K
{44 
var55 
user55 
=55 
await55 
_userRepository55 $
.55$ %
GetUserByUsername55% 6
(556 7
ubicationDelete557 F
.55F G
Username55G O
)55O P
.55P Q
ConfigureAwait55Q _
(55_ `
false55` e
)55e f
;55f g
if66 
(66 
user66 
==66 
null66 
)66 
{77 
return88 
false88 
;88 
}99 
var:: 
result:: 
=:: 
await::  
_ubicationRepository:: +
.::+ ,
DeleteUbication::, ;
(::; <
ubicationDelete::< K
)::K L
.::L M
ConfigureAwait::M [
(::[ \
false::\ a
)::a b
;::b c
return;; 

result;; 
;;; 
}<< 
public== 
async==	 
Task== 
<== 
object== 
>== 
GetUbicationDetails== /
(==/ 0
int==0 3
ubicationId==4 ?
,==? @
string==A G
stationType==H S
)==S T
{>> 
return?? 

stationType?? 
switch?? 
{@@ "
UbicationTypeConstantsAA 
.AA 
BICINGAA #
=>AA$ &
awaitAA' ,&
GetBicingStationWithStatusAA- G
(AAG H
ubicationIdAAH S
)AAS T
.AAT U
ConfigureAwaitAAU c
(AAc d
falseAAd i
)AAi j
,AAj k"
UbicationTypeConstantsBB 
.BB 
BUSBB  
=>BB! #
awaitBB$ )
_tmbServiceBB* 5
.BB5 6
GetBusByIdAsyncBB6 E
(BBE F
ubicationIdBBF Q
)BBQ R
.BBR S
ConfigureAwaitBBS a
(BBa b
falseBBb g
)BBg h
,BBh i"
UbicationTypeConstantsCC 
.CC 
METROCC "
=>CC# %
awaitCC& +
_tmbServiceCC, 7
.CC7 8
GetMetroByIdAsyncCC8 I
(CCI J
ubicationIdCCJ U
)CCU V
.CCV W
ConfigureAwaitCCW e
(CCe f
falseCCf k
)CCk l
,CCl m"
UbicationTypeConstantsDD 
.DD 
CHARGINGDD %
=>DD& (
awaitDD) .'
_chargingStationsRepositoryDD/ J
.DDJ K%
GetChargingStationDetailsDDK d
(DDd e
ubicationIdDDe p
)DDp q
.DDq r
ConfigureAwait	DDr Ä
(
DDÄ Å
false
DDÅ Ü
)
DDÜ á
,
DDá à
_EE 
=>EE 

nullEE 
,EE 
}FF 
;FF 
}GG 
publicHH 
asyncHH	 
TaskHH 
<HH 
boolHH 
>HH 
UpdateUbicationHH )
(HH) *
UbicationInfoDtoHH* :
savedUbicationHH; I
)HHI J
{II 
varJJ 
userJJ 
=JJ 
awaitJJ 
_userRepositoryJJ $
.JJ$ %
GetUserByUsernameJJ% 6
(JJ6 7
savedUbicationJJ7 E
.JJE F
UsernameJJF N
)JJN O
.JJO P
ConfigureAwaitJJP ^
(JJ^ _
falseJJ_ d
)JJd e
;JJe f
ifKK 
(KK 
userKK 
==KK 
nullKK 
)KK 
{LL 
returnMM 
falseMM 
;MM 
}NN 
varOO 
resultOO 
=OO 
awaitOO  
_ubicationRepositoryOO +
.OO+ ,
UpdateUbicationOO, ;
(OO; <
savedUbicationOO< J
)OOJ K
.OOK L
ConfigureAwaitOOL Z
(OOZ [
falseOO[ `
)OO` a
;OOa b
returnPP 

resultPP 
;PP 
}QQ 
privateSS 	
asyncSS
 
TaskSS 
<SS 
objectSS 
?SS 
>SS &
GetBicingStationWithStatusSS 8
(SS8 9
intSS9 <
	stationIdSS= F
)SSF G
{TT 
varUU 
infoUU 
=UU 
awaitUU $
_bicingStationRepositoryUU -
.UU- .#
GetBicingStationDetailsUU. E
(UUE F
	stationIdUUF O
)UUO P
.UUP Q
ConfigureAwaitUUQ _
(UU_ `
falseUU` e
)UUe f
;UUf g
varVV 
statusVV 
=VV 
awaitVV "
_stateBicingRepositoryVV -
.VV- .
GetStateBicingByIdVV. @
(VV@ A
	stationIdVVA J
)VVJ K
.VVK L
ConfigureAwaitVVL Z
(VVZ [
falseVV[ `
)VV` a
;VVa b
returnXX 

newXX 
{YY 
stationInfoZZ 
=ZZ 
infoZZ 
,ZZ 
realTimeStatus[[ 
=[[ 
status[[ 
}\\ 
;\\ 
}]] 
}__ Å¬
O/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/TmbService.cs
	namespace 	
Services
 
; 
public 
class 

TmbService 
: 
ITmbService %
{ 
private 	
readonly
 

HttpClient 
_httpClient )
;) *
private 	
readonly
 
IConfiguration !
_configuration" 0
;0 1
private 	
readonly
 
string 
_apiKey !
;! "
private 	
readonly
 
string 
_appId  
;  !
private 	
readonly
 
string 
_baseUrl "
;" #
public 

TmbService	 
( 

HttpClient 

httpClient )
,) *
IConfiguration+ 9
configuration: G
)G H
{ 
_httpClient 
= 

httpClient 
; 
_configuration 
= 
configuration "
;" #
_apiKey 
= 
_configuration 
[ 
$str ,
], -
;- .
_appId 

= 
_configuration 
[ 
$str *
]* +
;+ ,
_baseUrl 
= 
_configuration 
[ 
$str .
]. /
;/ 0
if 
( 
string 
. 
IsNullOrEmpty 
( 
_apiKey $
)$ %
||& (
string) /
./ 0
IsNullOrEmpty0 =
(= >
_appId> D
)D E
||F H
stringI O
.O P
IsNullOrEmptyP ]
(] ^
_baseUrl^ f
)f g
)g h
{ 
throw   
new   
	Exception   
(   
$str   `
)  ` a
;  a b
}!! 
}"" 
public$$ 
async$$	 
Task$$ 
<$$ 
List$$ 
<$$ 
MetroDto$$ !
>$$! "
>$$" #
GetAllMetrosAsync$$$ 5
($$5 6
)$$6 7
{%% 
var&& 
metrosEndpoint&& 
=&& 
_configuration&& '
[&&' (
$str&&( 7
]&&7 8
;&&8 9
var'' 

requestUrl'' 
='' 
$"'' 
{'' 
_baseUrl''  
}''  !
{''! "
metrosEndpoint''" 0
}''0 1
$str''1 9
{''9 :
_appId'': @
}''@ A
$str''A J
{''J K
_apiKey''K R
}''R S
"''S T
;''T U
var)) 
metrosResponse)) 
=)) 
await)) 
_httpClient)) *
.))* +
GetAsync))+ 3
())3 4

requestUrl))4 >
)))> ?
.))? @
ConfigureAwait))@ N
())N O
false))O T
)))T U
;))U V
if++ 
(++ 
!++ 	
metrosResponse++	 
.++ 
IsSuccessStatusCode++ +
)+++ ,
{,, 
Console-- 
.-- 
	WriteLine-- 
(-- 
$"-- 
$str-- +
{--+ ,
metrosResponse--, :
.--: ;

StatusCode--; E
}--E F
"--F G
)--G H
;--H I
return.. 
null.. 
;.. 
}// 
var11 
metrosContent11 
=11 
await11 
metrosResponse11 ,
.11, -
Content11- 4
.114 5
ReadAsStringAsync115 F
(11F G
)11G H
.11H I
ConfigureAwait11I W
(11W X
false11X ]
)11] ^
;11^ _
if33 
(33 
string33 
.33 
IsNullOrWhiteSpace33 !
(33! "
metrosContent33" /
)33/ 0
)330 1
{44 
Console55 
.55 
	WriteLine55 
(55 
$str55 >
)55> ?
;55? @
return66 
new66 
List66 
<66 
MetroDto66 
>66 
(66  
)66  !
;66! "
}77 
var99 

jsonObject99 
=99 
JObject99 
.99 
Parse99 "
(99" #
metrosContent99# 0
)990 1
;991 2
if:: 
(:: 
!:: 	

jsonObject::	 
.:: 
ContainsKey:: 
(::  
$str::  *
)::* +
)::+ ,
{;; 
Console<< 
.<< 
	WriteLine<< 
(<< 
$str<< >
)<<> ?
;<<? @
return== 
new== 
List== 
<== 
MetroDto== 
>== 
(==  
)==  !
;==! "
}>> 
var@@ 
metroStationsJson@@ 
=@@ 

jsonObject@@ &
[@@& '
$str@@' 1
]@@1 2
as@@3 5
JArray@@6 <
;@@< =
ifAA 
(AA 
metroStationsJsonAA 
==AA 
nullAA !
||AA" $
!AA% &
metroStationsJsonAA& 7
.AA7 8
AnyAA8 ;
(AA; <
)AA< =
)AA= >
{BB 
ConsoleCC 
.CC 
	WriteLineCC 
(CC 
$strCC F
)CCF G
;CCG H
returnDD 
newDD 
ListDD 
<DD 
MetroDtoDD 
>DD 
(DD  
)DD  !
;DD! "
}EE 
ListGG 
<GG 	
MetroDtoGG	 
>GG 
	metroListGG 
=GG 
newGG "
ListGG# '
<GG' (
MetroDtoGG( 0
>GG0 1
(GG1 2
)GG2 3
;GG3 4
foreachII 
(II 
varII 
metroII 
inII 
metroStationsJsonII +
)II+ ,
{JJ 
varKK 	

propertiesKK
 
=KK 
metroKK 
[KK 
$strKK )
]KK) *
;KK* +
varLL 	
geometryLL
 
=LL 
metroLL 
[LL 
$strLL %
]LL% &
;LL& '
MetroDtoNN 
metroStationNN 
=NN 
newNN !
MetroDtoNN" *
{OO 
IdEstacioLiniaPP 
=PP 

propertiesPP #
?PP# $
[PP$ %
$strPP% 7
]PP7 8
?PP8 9
.PP9 :
ValuePP: ?
<PP? @
intPP@ C
>PPC D
(PPD E
)PPE F
??PPG I
$numPPJ K
,PPK L
CodiEstacioLiniaQQ 
=QQ 

propertiesQQ %
?QQ% &
[QQ& '
$strQQ' ;
]QQ; <
?QQ< =
.QQ= >
ValueQQ> C
<QQC D
intQQD G
>QQG H
(QQH I
)QQI J
??QQK M
$numQQN O
,QQO P
IdGrupEstacioRR 
=RR 

propertiesRR "
?RR" #
[RR# $
$strRR$ 5
]RR5 6
?RR6 7
.RR7 8
ValueRR8 =
<RR= >
intRR> A
>RRA B
(RRB C
)RRC D
??RRE G
$numRRH I
,RRI J
CodiGrupEstacioSS 
=SS 

propertiesSS $
?SS$ %
[SS% &
$strSS& 9
]SS9 :
?SS: ;
.SS; <
ValueSS< A
<SSA B
intSSB E
>SSE F
(SSF G
)SSG H
??SSI K
$numSSL M
,SSM N
	IdEstacioTT 
=TT 

propertiesTT 
?TT 
[TT  
$strTT  ,
]TT, -
?TT- .
.TT. /
ValueTT/ 4
<TT4 5
intTT5 8
>TT8 9
(TT9 :
)TT: ;
??TT< >
$numTT? @
,TT@ A
CodiEstacioUU 
=UU 

propertiesUU  
?UU  !
[UU! "
$strUU" 0
]UU0 1
?UU1 2
.UU2 3
ValueUU3 8
<UU8 9
intUU9 <
>UU< =
(UU= >
)UU> ?
??UU@ B
$numUUC D
,UUD E

NomEstacioVV 
=VV 

propertiesVV 
?VV  
[VV  !
$strVV! .
]VV. /
?VV/ 0
.VV0 1
ValueVV1 6
<VV6 7
stringVV7 =
>VV= >
(VV> ?
)VV? @
??VVA C
$strVVD Q
,VVQ R
OrdreEstacioWW 
=WW 

propertiesWW !
?WW! "
[WW" #
$strWW# 2
]WW2 3
?WW3 4
.WW4 5
ValueWW5 :
<WW: ;
intWW; >
>WW> ?
(WW? @
)WW@ A
??WWB D
$numWWE F
,WWF G
IdLiniaXX 
=XX 

propertiesXX 
?XX 
[XX 
$strXX (
]XX( )
?XX) *
.XX* +
ValueXX+ 0
<XX0 1
intXX1 4
>XX4 5
(XX5 6
)XX6 7
??XX8 :
$numXX; <
,XX< =
	CodiLiniaYY 
=YY 

propertiesYY 
?YY 
[YY  
$strYY  ,
]YY, -
?YY- .
.YY. /
ValueYY/ 4
<YY4 5
intYY5 8
>YY8 9
(YY9 :
)YY: ;
??YY< >
$numYY? @
,YY@ A
NomLiniaZZ 
=ZZ 

propertiesZZ 
?ZZ 
[ZZ 
$strZZ *
]ZZ* +
?ZZ+ ,
.ZZ, -
ValueZZ- 2
<ZZ2 3
stringZZ3 9
>ZZ9 :
(ZZ: ;
)ZZ; <
??ZZ= ?
$strZZ@ M
,ZZM N

DescServei[[ 
=[[ 

properties[[ 
?[[  
[[[  !
$str[[! .
][[. /
?[[/ 0
.[[0 1
Value[[1 6
<[[6 7
string[[7 =
>[[= >
([[> ?
)[[? @
??[[A C
$str[[D Q
,[[Q R
OrigenServei\\ 
=\\ 

properties\\ !
?\\! "
[\\" #
$str\\# 2
]\\2 3
?\\3 4
.\\4 5
Value\\5 :
<\\: ;
string\\; A
>\\A B
(\\B C
)\\C D
??\\E G
$str\\H U
,\\U V
DestiServei]] 
=]] 

properties]]  
?]]  !
[]]! "
$str]]" 0
]]]0 1
?]]1 2
.]]2 3
Value]]3 8
<]]8 9
string]]9 ?
>]]? @
(]]@ A
)]]A B
??]]C E
$str]]F S
,]]S T

ColorLinia^^ 
=^^ 

properties^^ 
?^^  
[^^  !
$str^^! .
]^^. /
?^^/ 0
.^^0 1
Value^^1 6
<^^6 7
string^^7 =
>^^= >
(^^> ?
)^^? @
??^^A C
$str^^D Q
,^^Q R
Picto__ 
=__ 

properties__ 
?__ 
[__ 
$str__ #
]__# $
?__$ %
.__% &
Value__& +
<__+ ,
string__, 2
>__2 3
(__3 4
)__4 5
??__6 8
$str__9 F
,__F G
	PictoGrup`` 
=`` 

properties`` 
?`` 
[``  
$str``  ,
]``, -
?``- .
.``. /
Value``/ 4
<``4 5
string``5 ;
>``; <
(``< =
)``= >
??``? A
$str``B O
,``O P
DataInauguracioaa 
=aa 

propertiesaa $
?aa$ %
[aa% &
$straa& 8
]aa8 9
?aa9 :
.aa: ;
Valueaa; @
<aa@ A
stringaaA G
>aaG H
(aaH I
)aaI J
??aaK M
$straaN [
,aa[ \
Databb 
=bb 

propertiesbb 
?bb 
[bb 
$strbb !
]bb! "
?bb" #
.bb# $
Valuebb$ )
<bb) *
stringbb* 0
>bb0 1
(bb1 2
)bb2 3
??bb4 6
$strbb7 D
,bbD E
Latitudecc 
=cc 
geometrycc 
?cc 
[cc 
$strcc *
]cc* +
?cc+ ,
[cc, -
$numcc- .
]cc. /
?cc/ 0
.cc0 1
Valuecc1 6
<cc6 7
doublecc7 =
>cc= >
(cc> ?
)cc? @
??ccA C
$numccD G
,ccG H
	Longitudedd 
=dd 
geometrydd 
?dd 
[dd 
$strdd +
]dd+ ,
?dd, -
[dd- .
$numdd. /
]dd/ 0
?dd0 1
.dd1 2
Valuedd2 7
<dd7 8
doubledd8 >
>dd> ?
(dd? @
)dd@ A
??ddB D
$numddE H
}ee 
;ee 
	metroListgg 
.gg 
Addgg 
(gg 
metroStationgg  
)gg  !
;gg! "
}hh 
returnjj 

	metroListjj 
;jj 
}kk 
publicmm 
asyncmm	 
Taskmm 
<mm 
Listmm 
<mm 
BusDtomm 
>mm  
>mm  !
GetAllBusAsyncmm" 0
(mm0 1
)mm1 2
{nn 
varoo 
busEndpointoo 
=oo 
_configurationoo $
[oo$ %
$stroo% 1
]oo1 2
;oo2 3
varpp 

requestUrlpp 
=pp 
$"pp 
{pp 
_baseUrlpp  
}pp  !
{pp! "
busEndpointpp" -
}pp- .
$strpp. 6
{pp6 7
_appIdpp7 =
}pp= >
$strpp> G
{ppG H
_apiKeyppH O
}ppO P
"ppP Q
;ppQ R
varrr 
busResponserr 
=rr 
awaitrr 
_httpClientrr '
.rr' (
GetAsyncrr( 0
(rr0 1

requestUrlrr1 ;
)rr; <
.rr< =
ConfigureAwaitrr= K
(rrK L
falserrL Q
)rrQ R
;rrR S
iftt 
(tt 
!tt 	
busResponsett	 
.tt 
IsSuccessStatusCodett (
)tt( )
{uu 
Consolevv 
.vv 
	WriteLinevv 
(vv 
$"vv 
$strvv +
{vv+ ,
busResponsevv, 7
.vv7 8

StatusCodevv8 B
}vvB C
"vvC D
)vvD E
;vvE F
returnww 
nullww 
;ww 
}xx 
varzz 

busContentzz 
=zz 
awaitzz 
busResponsezz &
.zz& '
Contentzz' .
.zz. /
ReadAsStringAsynczz/ @
(zz@ A
)zzA B
.zzB C
ConfigureAwaitzzC Q
(zzQ R
falsezzR W
)zzW X
;zzX Y
var|| 
busStopsJson|| 
=|| 
JObject|| 
.|| 
Parse|| $
(||$ %

busContent||% /
)||/ 0
[||0 1
$str||1 ;
]||; <
as||= ?
JArray||@ F
;||F G
if}} 
(}} 
busStopsJson}} 
==}} 
null}} 
)}} 
{~~ 
Console 
. 
	WriteLine 
( 
$str E
)E F
;F G
return
ÄÄ 
new
ÄÄ 
List
ÄÄ 
<
ÄÄ 
BusDto
ÄÄ 
>
ÄÄ 
(
ÄÄ 
)
ÄÄ 
;
ÄÄ  
}
ÅÅ 
List
ÉÉ 
<
ÉÉ 	
BusDto
ÉÉ	 
>
ÉÉ 
busList
ÉÉ 
=
ÉÉ 
new
ÉÉ 
List
ÉÉ #
<
ÉÉ# $
BusDto
ÉÉ$ *
>
ÉÉ* +
(
ÉÉ+ ,
)
ÉÉ, -
;
ÉÉ- .
foreach
ÖÖ 
(
ÖÖ 
var
ÖÖ 
busStop
ÖÖ 
in
ÖÖ 
busStopsJson
ÖÖ (
)
ÖÖ( )
{
ÜÜ 
var
áá 	

properties
áá
 
=
áá 
busStop
áá 
[
áá 
$str
áá +
]
áá+ ,
;
áá, -
var
àà 	
geometry
àà
 
=
àà 
busStop
àà 
[
àà 
$str
àà '
]
àà' (
;
àà( )
BusDto
ää 

busStation
ää 
=
ää 
new
ää 
BusDto
ää $
{
ãã 
ParadaId
åå 
=
åå 

properties
åå 
?
åå 
[
åå 
$str
åå *
]
åå* +
?
åå+ ,
.
åå, -
Value
åå- 2
<
åå2 3
int
åå3 6
>
åå6 7
(
åå7 8
)
åå8 9
??
åå: <
$num
åå= >
,
åå> ?

CodiParada
çç 
=
çç 

properties
çç 
?
çç  
[
çç  !
$str
çç! .
]
çç. /
?
çç/ 0
.
çç0 1
Value
çç1 6
<
çç6 7
int
çç7 :
>
çç: ;
(
çç; <
)
çç< =
??
çç> @
$num
ççA B
,
ççB C
Name
éé 
=
éé 

properties
éé 
?
éé 
[
éé 
$str
éé '
]
éé' (
?
éé( )
.
éé) *
Value
éé* /
<
éé/ 0
string
éé0 6
>
éé6 7
(
éé7 8
)
éé8 9
??
éé: <
$str
éé= J
,
ééJ K
Description
èè 
=
èè 

properties
èè  
?
èè  !
[
èè! "
$str
èè" /
]
èè/ 0
?
èè0 1
.
èè1 2
Value
èè2 7
<
èè7 8
string
èè8 >
>
èè> ?
(
èè? @
)
èè@ A
??
èèB D
$str
èèE R
,
èèR S
IntersectionId
êê 
=
êê 

properties
êê #
?
êê# $
[
êê$ %
$str
êê% 2
]
êê2 3
?
êê3 4
.
êê4 5
Value
êê5 :
<
êê: ;
int
êê; >
>
êê> ?
(
êê? @
)
êê@ A
??
êêB D
$num
êêE F
,
êêF G
IntersectionName
ëë 
=
ëë 

properties
ëë %
?
ëë% &
[
ëë& '
$str
ëë' 3
]
ëë3 4
?
ëë4 5
.
ëë5 6
Value
ëë6 ;
<
ëë; <
string
ëë< B
>
ëëB C
(
ëëC D
)
ëëD E
??
ëëF H
$str
ëëI V
,
ëëV W
ParadaTypeName
íí 
=
íí 

properties
íí #
?
íí# $
[
íí$ %
$str
íí% 7
]
íí7 8
?
íí8 9
.
íí9 :
Value
íí: ?
<
íí? @
string
íí@ F
>
ííF G
(
ííG H
)
ííH I
??
ííJ L
$str
ííM Z
,
ííZ [$
ParadaTypeTipification
ìì 
=
ìì  

properties
ìì! +
?
ìì+ ,
[
ìì, -
$str
ìì- A
]
ììA B
?
ììB C
.
ììC D
Value
ììD I
<
ììI J
string
ììJ P
>
ììP Q
(
ììQ R
)
ììR S
??
ììT V
$str
ììW d
,
ììd e
Adress
îî 
=
îî 

properties
îî 
?
îî 
[
îî 
$str
îî %
]
îî% &
?
îî& '
.
îî' (
Value
îî( -
<
îî- .
string
îî. 4
>
îî4 5
(
îî5 6
)
îî6 7
??
îî8 :
$str
îî; H
,
îîH I

LocationId
ïï 
=
ïï 

properties
ïï 
?
ïï  
[
ïï  !
$str
ïï! .
]
ïï. /
?
ïï/ 0
.
ïï0 1
Value
ïï1 6
<
ïï6 7
string
ïï7 =
>
ïï= >
(
ïï> ?
)
ïï? @
??
ïïA C
$str
ïïD Q
,
ïïQ R
LocationName
ññ 
=
ññ 

properties
ññ !
?
ññ! "
[
ññ" #
$str
ññ# 1
]
ññ1 2
?
ññ2 3
.
ññ3 4
Value
ññ4 9
<
ññ9 :
string
ññ: @
>
ññ@ A
(
ññA B
)
ññB C
??
ññD F
$str
ññG T
,
ññT U

DistrictId
óó 
=
óó 

properties
óó 
?
óó  
[
óó  !
$str
óó! /
]
óó/ 0
?
óó0 1
.
óó1 2
Value
óó2 7
<
óó7 8
string
óó8 >
>
óó> ?
(
óó? @
)
óó@ A
??
óóB D
$str
óóE R
,
óóR S
DistrictName
òò 
=
òò 

properties
òò !
?
òò! "
[
òò" #
$str
òò# 2
]
òò2 3
?
òò3 4
.
òò4 5
Value
òò5 :
<
òò: ;
string
òò; A
>
òòA B
(
òòB C
)
òòC D
??
òòE G
$str
òòH U
,
òòU V
Date
ôô 
=
ôô 

properties
ôô 
?
ôô 
[
ôô 
$str
ôô !
]
ôô! "
?
ôô" #
.
ôô# $
Value
ôô$ )
<
ôô) *
string
ôô* 0
>
ôô0 1
(
ôô1 2
)
ôô2 3
??
ôô4 6
$str
ôô7 D
,
ôôD E

StreetName
öö 
=
öö 

properties
öö 
?
öö  
[
öö  !
$str
öö! *
]
öö* +
?
öö+ ,
.
öö, -
Value
öö- 2
<
öö2 3
string
öö3 9
>
öö9 :
(
öö: ;
)
öö; <
??
öö= ?
$str
öö@ M
,
ööM N
ParadaPoints
õõ 
=
õõ 

properties
õõ !
?
õõ! "
[
õõ" #
$str
õõ# 1
]
õõ1 2
?
õõ2 3
.
õõ3 4
Value
õõ4 9
<
õõ9 :
string
õõ: @
>
õõ@ A
(
õõA B
)
õõB C
??
õõD F
$str
õõG T
,
õõT U
Latitude
úú 
=
úú 
geometry
úú 
?
úú 
[
úú 
$str
úú *
]
úú* +
?
úú+ ,
[
úú, -
$num
úú- .
]
úú. /
?
úú/ 0
.
úú0 1
Value
úú1 6
<
úú6 7
double
úú7 =
>
úú= >
(
úú> ?
)
úú? @
??
úúA C
$num
úúD G
,
úúG H
	Longitude
ùù 
=
ùù 
geometry
ùù 
?
ùù 
[
ùù 
$str
ùù +
]
ùù+ ,
?
ùù, -
[
ùù- .
$num
ùù. /
]
ùù/ 0
?
ùù0 1
.
ùù1 2
Value
ùù2 7
<
ùù7 8
double
ùù8 >
>
ùù> ?
(
ùù? @
)
ùù@ A
??
ùùB D
$num
ùùE H
}
ûû 
;
ûû 
busList
†† 
.
†† 
Add
†† 
(
†† 

busStation
†† 
)
†† 
;
†† 
}
°° 
return
££ 

busList
££ 
;
££ 
}
§§ 
public
•• 
async
••	 
Task
•• 
<
•• 
MetroDto
•• 
?
•• 
>
•• 
GetMetroByIdAsync
•• 0
(
••0 1
int
••1 4
id
••5 7
)
••7 8
{
¶¶ 
var
ßß 
	allMetros
ßß 
=
ßß 
await
ßß 
GetAllMetrosAsync
ßß +
(
ßß+ ,
)
ßß, -
.
ßß- .
ConfigureAwait
ßß. <
(
ßß< =
false
ßß= B
)
ßßB C
;
ßßC D
return
®® 

	allMetros
®® 
?
®® 
.
®® 
FirstOrDefault
®® $
(
®®$ %
m
®®% &
=>
®®' )
m
®®* +
.
®®+ ,
	IdEstacio
®®, 5
==
®®6 8
id
®®9 ;
)
®®; <
;
®®< =
}
©© 
public
™™ 
async
™™	 
Task
™™ 
<
™™ 
BusDto
™™ 
?
™™ 
>
™™ 
GetBusByIdAsync
™™ ,
(
™™, -
int
™™- 0
id
™™1 3
)
™™3 4
{
´´ 
var
¨¨ 
allBuses
¨¨ 
=
¨¨ 
await
¨¨ 
GetAllBusAsync
¨¨ '
(
¨¨' (
)
¨¨( )
.
¨¨) *
ConfigureAwait
¨¨* 8
(
¨¨8 9
false
¨¨9 >
)
¨¨> ?
;
¨¨? @
return
≠≠ 

allBuses
≠≠ 
?
≠≠ 
.
≠≠ 
FirstOrDefault
≠≠ #
(
≠≠# $
b
≠≠$ %
=>
≠≠& (
b
≠≠) *
.
≠≠* +
ParadaId
≠≠+ 3
==
≠≠4 6
id
≠≠7 9
)
≠≠9 :
;
≠≠: ;
}
ÆÆ 
}ØØ ™D
W/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/StateBicingService.cs
	namespace 	
Services
 
; 
public 
class 
StateBicingService 
:  !
IStateBicingService" 5
{ 
private 	
readonly
 "
IStateBicingRepository )"
_stateBicingRepository* @
;@ A
private 	
readonly
 

HttpClient 
_httpClient )
;) *
private 	
readonly
 
ILogger 
< 
StateBicingService -
>- .
_logger/ 6
;6 7
private 	
readonly
 
string 
	_apiToken #
=$ %
$str& h
;h i
private 	
JsonElement
 
? 
_cachedJsonData &
;& '
private 	
DateTime
 
_lastFetchTime !
=" #
DateTime$ ,
., -
MinValue- 5
;5 6
private 	
readonly
 
TimeSpan 
_cacheDuration *
=+ ,
TimeSpan- 5
.5 6
FromMinutes6 A
(A B
$numB D
)D E
;E F
public 
StateBicingService	 
( 

HttpClient &

httpClient' 1
,1 2
ILogger3 :
<: ;
StateBicingService; M
>M N
loggerO U
,U V"
IStateBicingRepositoryW m#
dadesObertesRepository	n Ñ
)
Ñ Ö
{ 
_httpClient 
= 

httpClient 
; 
_logger 
= 
logger 
; "
_stateBicingRepository 
= "
dadesObertesRepository 3
;3 4
}   
public"" 
async""	 
Task"" 
<"" 
List"" 
<"" 
StateBicingDto"" '
>""' (
>""( )*
GetAllStateBicingStationsAsync""* H
(""H I
)""I J
{## 
return$$ 

await$$ "
_stateBicingRepository$$ '
.$$' (%
GetAllStateBicingStations$$( A
($$A B
)$$B C
.$$C D
ConfigureAwait$$D R
($$R S
false$$S X
)$$X Y
;$$Y Z
}%% 
public'' 
async''	 
Task'' 1
%FetchAndStoreStateBicingStationsAsync'' 9
(''9 :
)'': ;
{(( 
try)) 
{** 
if++ 
(++	 

_cachedJsonData++
 
==++ 
null++ !
||++" $
DateTime++% -
.++- .
Now++. 1
-++2 3
_lastFetchTime++4 B
>++C D
_cacheDuration++E S
)++S T
{,, 
var-- 

requestUrl-- 
=-- 
$str	-- Æ
;
--Æ Ø
_httpClient.. 
... !
DefaultRequestHeaders.. )
...) *
Authorization..* 7
=..8 9
new..: =%
AuthenticationHeaderValue..> W
(..W X
	_apiToken..X a
)..a b
;..b c
var00 
response00 
=00 
await00 
_httpClient00 (
.00( )
GetAsync00) 1
(001 2

requestUrl002 <
)00< =
.00= >
ConfigureAwait00> L
(00L M
false00M R
)00R S
;00S T
response11 
.11 #
EnsureSuccessStatusCode11 (
(11( )
)11) *
;11* +
var33 
jsonResponse33 
=33 
await33  
response33! )
.33) *
Content33* 1
.331 2
ReadAsStringAsync332 C
(33C D
)33D E
.33E F
ConfigureAwait33F T
(33T U
false33U Z
)33Z [
;33[ \
_cachedJsonData55 
=55 
JsonSerializer55 (
.55( )
Deserialize55) 4
<554 5
JsonElement555 @
>55@ A
(55A B
jsonResponse55B N
)55N O
;55O P
_lastFetchTime66 
=66 
DateTime66 !
.66! "
Now66" %
;66% &
}77 
if99 
(99	 

!99
 
_cachedJsonData99 
.99 
HasValue99 #
||99$ &
!99' (
_cachedJsonData99( 7
.997 8
Value998 =
.99= >
TryGetProperty99> L
(99L M
$str99M S
,99S T
out99U X
var99Y \
dataElement99] h
)99h i
)99i j
{:: 
_logger;; 
.;; 
LogError;; 
(;; 
$str;; I
);;I J
;;;J K
return<< 
;<< 
}== 
var?? 	
stateBicingToAdd??
 
=?? 
new??  
List??! %
<??% &
StateBicingEntity??& 7
>??7 8
(??8 9
)??9 :
;??: ;
ifAA 
(AA	 

dataElementAA
 
.AA 
TryGetPropertyAA $
(AA$ %
$strAA% /
,AA/ 0
outAA1 4
varAA5 8
statesElementAA9 F
)AAF G
)AAG H
{BB 
foreachCC 
(CC 
varCC 
stateElementCC !
inCC" $
statesElementCC% 2
.CC2 3
EnumerateArrayCC3 A
(CCA B
)CCB C
)CCC D
{DD 	
varEE
 
BicingidEE 
=EE 
stateElementEE %
.EE% &
GetPropertyEE& 1
(EE1 2
$strEE2 >
)EE> ?
.EE? @
GetInt32EE@ H
(EEH I
)EEI J
;EEJ K
stateBicingToAddGG
 
.GG 
AddGG 
(GG 
newGG "
StateBicingEntityGG# 4
{HH
 
BicingIdII 
=II 
BicingidII 
,II  
NumBikesAvailableJJ 
=JJ 
stateElementJJ  ,
.JJ, -
GetPropertyJJ- 8
(JJ8 9
$strJJ9 N
)JJN O
.JJO P
GetInt32JJP X
(JJX Y
)JJY Z
,JJZ ['
NumBikesAvailableMechanicalKK '
=KK( )
stateElementKK* 6
.KK6 7
GetPropertyKK7 B
(KKB C
$strKKC ^
)KK^ _
.KK_ `
GetPropertyKK` k
(KKk l
$strKKl x
)KKx y
.KKy z
GetInt32	KKz Ç
(
KKÇ É
)
KKÉ Ñ
,
KKÑ Ö"
NumBikesAvailableEbikeLL "
=LL# $
stateElementLL% 1
.LL1 2
GetPropertyLL2 =
(LL= >
$strLL> Y
)LLY Z
.LLZ [
GetPropertyLL[ f
(LLf g
$strLLg n
)LLn o
.LLo p
GetInt32LLp x
(LLx y
)LLy z
,LLz {
NumDocksAvailableMM 
=MM 
stateElementMM  ,
.MM, -
GetPropertyMM- 8
(MM8 9
$strMM9 N
)MMN O
.MMO P
GetInt32MMP X
(MMX Y
)MMY Z
,MMZ [
LastReportedNN 
=NN 
DateTimeOffsetNN )
.NN) *
FromUnixTimeSecondsNN* =
(NN= >
stateElementNN> J
.NNJ K
GetPropertyNNK V
(NNV W
$strNNW f
)NNf g
.NNg h
GetInt64NNh p
(NNp q
)NNq r
)NNr s
.NNs t
DateTimeNNt |
,NN| }
StatusOO 
=OO 
stateElementOO !
.OO! "
GetPropertyOO" -
(OO- .
$strOO. 6
)OO6 7
.OO7 8
	GetStringOO8 A
(OOA B
)OOB C
}PP
 
)PP 
;PP 
}QQ 	
}RR 
awaitSS "
_stateBicingRepositorySS "
.SS" #
BulkInsertAsyncSS# 2
(SS2 3
stateBicingToAddSS3 C
)SSC D
.SSD E
ConfigureAwaitSSE S
(SSS T
falseSST Y
)SSY Z
;SSZ [
_loggerTT 
.TT 
LogInformationTT 
(TT 
$strTT @
,TT@ A
stateBicingToAddUU
 
.UU 
CountUU  
)UU  !
;UU! "
}VV 
catchWW 	
(WW
 
	ExceptionWW 
exWW 
)WW 
{XX 
_loggerYY 
.YY 
LogErrorYY 
(YY 
exYY 
,YY 
$strYY g
)YYg h
;YYh i
throwZZ 
;ZZ 
}[[ 
}\\ 
}]] ñ 
a/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/StateBicingBackgroundService.cs
	namespace		 	
Services		
 
;		 
public 
class (
StateBicingBackgroundService )
:* +
BackgroundService, =
{ 
private 	
readonly
 
IServiceProvider #
_serviceProvider$ 4
;4 5
private 	
readonly
 
ILogger 
< (
StateBicingBackgroundService 7
>7 8
_logger9 @
;@ A
private 	
readonly
 
TimeSpan 
	_interval %
=& '
TimeSpan( 0
.0 1
FromMinutes1 <
(< =
$num= ?
)? @
;@ A
public (
StateBicingBackgroundService	 %
(% &
IServiceProvider 
serviceProvider &
,& '
ILogger 
< (
StateBicingBackgroundService *
>* +
logger, 2
)2 3
{ 
_serviceProvider 
= 
serviceProvider &
;& '
_logger 
= 
logger 
; 
} 
	protected 
override 
async 
Task 
ExecuteAsync  ,
(, -
CancellationToken- >
stoppingToken? L
)L M
{ 
while 	
(
 
! 
stoppingToken 
. #
IsCancellationRequested 1
)1 2
{ 
try 	
{ 
const 
int 
maxWaitTimeSeconds $
=% &
$num' )
;) *
const   
int   
checkIntervalMs   !
=  " #
$num  $ (
;  ( )
int!! 
elapsedSeconds!! 
=!! 
$num!! 
;!! 
while$$ 
($$ 
elapsedSeconds$$ 
<$$ 
maxWaitTimeSeconds$$  2
)$$2 3
{%% 	
await&&
 
Task&& 
.&& 
Delay&& 
(&& 
checkIntervalMs&& *
,&&* +
stoppingToken&&, 9
)&&9 :
.&&: ;
ConfigureAwait&&; I
(&&I J
false&&J O
)&&O P
;&&P Q
elapsedSeconds''
 
++'' 
;'' 
}(( 	
_logger** 
.** 
LogInformation** 
(** 
$str** Y
)**Y Z
;**Z [
using-- 
(-- 
var-- 
scope-- 
=-- 
_serviceProvider-- +
.--+ ,
CreateScope--, 7
(--7 8
)--8 9
)--9 :
{.. 	
var//
 
stateBicingService//  
=//! "
scope//# (
.//( )
ServiceProvider//) 8
.//8 9
GetRequiredService//9 K
<//K L
IStateBicingService//L _
>//_ `
(//` a
)//a b
;//b c
await00
 
stateBicingService00 "
.00" #1
%FetchAndStoreStateBicingStationsAsync00# H
(00H I
)00I J
.00J K
ConfigureAwait00K Y
(00Y Z
false00Z _
)00_ `
;00` a
}11 	
_logger33 
.33 
LogInformation33 
(33 
$str33 Z
)33Z [
;33[ \
}44 
catch55 
(55 
	Exception55 
ex55 
)55 
{66 
_logger77 
.77 
LogError77 
(77 
ex77 
,77 
$str77 [
)77[ \
;77\ ]
}88 
await:: 
Task:: 
.:: 
Delay:: 
(:: 
	_interval::  
,::  !
stoppingToken::" /
)::/ 0
.::0 1
ConfigureAwait::1 ?
(::? @
false::@ E
)::E F
;::F G
};; 
}<< 
}== ìÓ
Q/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/RouteService.cs
public 
class 
RouteService 
: 
IRouteService )
{ 
private 	
readonly
 
IConfiguration !
_config" )
;) *
private 	
readonly
 

HttpClient 
_httpClient )
;) *
private 	
readonly
 
IRouteRepository #
_repo$ )
;) *
public 
RouteService	 
( 
IConfiguration $
config% +
,+ ,

HttpClient- 7

httpClient8 B
,B C
IRouteRepositoryD T
repoU Y
)Y Z
{ 
_config 
= 
config 
; 
_httpClient 
= 

httpClient 
; 
_repo 	
=
 
repo 
; 
} 
public 
async	 
Task 
< 
RouteResponseDto $
>$ %
CalcularRutaAsync& 7
(7 8
RouteRequestDto8 G
requestH O
,O P
GuidQ U
	usuarioIdV _
)_ `
{ 
var 
response 
= 
request 
. 
Mode 
==  "
$str# ,
? 	
await
 &
CalcularConGoogleMapsAsync *
(* +
request+ 2
)2 3
.3 4
ConfigureAwait4 B
(B C
falseC H
)H I
: 	
await
 
CalcularConORSAsync #
(# $
request$ +
)+ ,
., -
ConfigureAwait- ;
(; <
false< A
)A B
;B C
var 
route 
= 
new 
RouteEntity 
{   
Id!! 
=!!	 

Guid!! 
.!! 
NewGuid!! 
(!! 
)!! 
,!! 
	OriginLat"" 
="" 
request"" 
."" 
	OriginLat"" #
,""# $
	OriginLng## 
=## 
request## 
.## 
	OriginLng## #
,### $
DestinationLat$$ 
=$$ 
request$$ 
.$$ 
DestinationLat$$ -
,$$- .
DestinationLng%% 
=%% 
request%% 
.%% 
DestinationLng%% -
,%%- .
Mean&& 

=&& 
request&& 
.&& 
Mode&& 
,&& 

Preference'' 
='' 
request'' 
.'' 

Preference'' %
,''% &
Distance(( 
=(( 
((( 
float(( 
)(( 
response((  
.((  !
Distance((! )
,(() *
Duration)) 
=)) 
()) 
float)) 
))) 
response))  
.))  !
Duration))! )
,))) *
GeometryJson** 
=** 
JsonSerializer** #
.**# $
	Serialize**$ -
(**- .
response**. 6
.**6 7
Geometry**7 ?
)**? @
,**@ A
InstructionsJson++ 
=++ 
JsonSerializer++ '
.++' (
	Serialize++( 1
(++1 2
response++2 :
.++: ;
Instructions++; G
)++G H
},, 
;,, 
return00 

response00 
;00 
}11 
public33 
async33	 
Task33 
<33 
RouteResponseDto33 $
>33$ %
CalcularConORSAsync33& 9
(339 :
RouteRequestDto33: I
request33J Q
)33Q R
{44 
string55 

profile55 
=55 
request55 
.55 
Mode55 !
switch55" (
{66 
$str77 
=>77 
$str77 
,77 
$str88 
=>88 
$str88 
,88 
$str99 
=>99 
$str99 !
,99! "
$str:: 
=>:: 
$str:: #
,::# $
$str;; 
=>;; 
$str;; +
,;;+ ,
$str<< 
=><< 
$str<< +
,<<+ ,
$str== 
=>== 
$str== 
,== 
$str>> 
=>>> 
$str>> 
,>>  
$str?? 
=>?? 
$str?? (
,??( )
_@@ 
=>@@ 

$str@@ 
}AA 
;AA 
varCC 
apiKeyCC 
=CC 
_configCC 
[CC 
$strCC -
]CC- .
;CC. /
varDD 
urlDD 
=DD 
$"DD 
$strDD ?
{DD? @
profileDD@ G
}DDG H
"DDH I
;DDI J
_httpClientEE 
.EE !
DefaultRequestHeadersEE %
.EE% &
AddEE& )
(EE) *
$strEE* 9
,EE9 :
apiKeyEE; A
)EEA B
;EEB C
varGG 
bodyGG 
=GG 
newGG 
{HH 
coordinatesII 
=II 
newII 
[II 
]II 
{JJ 	
newKK 
[KK 
]KK 
{KK 
requestKK 
.KK  
	OriginLngKK  )
,KK) *
requestKK+ 2
.KK2 3
	OriginLatKK3 <
}KK= >
,KK> ?
newLL 
[LL 
]LL 
{LL 
requestLL 
.LL  
DestinationLngLL  .
,LL. /
requestLL0 7
.LL7 8
DestinationLatLL8 F
}LLG H
}MM 
,MM 

preferenceNN 
=NN 
requestNN 
.NN 

PreferenceNN %
switchNN& ,
{OO 
$strPP 
=>PP 
$strPP  
,PP  !
_QQ 	
=>QQ
 
requestQQ 
.QQ 
ModeQQ 
==QQ 
$strQQ *
?QQ+ ,
$strQQ- 7
:QQ8 9
$strQQ: G
}RR 
}TT 
;TT 
varVV 
contentVV 
=VV 
newVV 
StringContentVV #
(VV# $
JsonSerializerVV$ 2
.VV2 3
	SerializeVV3 <
(VV< =
bodyVV= A
)VVA B
,VVB C
EncodingVVD L
.VVL M
UTF8VVM Q
,VVQ R
$strVVS e
)VVe f
;VVf g
varWW 
resWW 
=WW 
awaitWW 
_httpClientWW 
.WW  
	PostAsyncWW  )
(WW) *
urlWW* -
,WW- .
contentWW/ 6
)WW6 7
.WW7 8
ConfigureAwaitWW8 F
(WWF G
falseWWG L
)WWL M
;WWM N
varXX 
jsonXX 
=XX 
awaitXX 
resXX 
.XX 
ContentXX  
.XX  !
ReadAsStringAsyncXX! 2
(XX2 3
)XX3 4
.XX4 5
ConfigureAwaitXX5 C
(XXC D
falseXXD I
)XXI J
;XXJ K
ConsoleYY 
.YY 
	WriteLineYY 
(YY 
$strYY &
+YY' (
jsonYY) -
)YY- .
;YY. /
using[[ 	
var[[
 
doc[[ 
=[[ 
JsonDocument[[  
.[[  !
Parse[[! &
([[& '
json[[' +
)[[+ ,
;[[, -
if\\ 
(\\ 
!\\ 	
doc\\	 
.\\ 
RootElement\\ 
.\\ 
TryGetProperty\\ '
(\\' (
$str\\( 0
,\\0 1
out\\2 5
var\\6 9
routes\\: @
)\\@ A
||\\B D
routes\\E K
.\\K L
GetArrayLength\\L Z
(\\Z [
)\\[ \
==\\] _
$num\\` a
)\\a b
{]] 
throw^^ 
new^^ 
	Exception^^ 
(^^ 
$str^^ [
)^^[ \
;^^\ ]
}__ 
varaa 
routeaa 
=aa 
routesaa 
[aa 
$numaa 
]aa 
;aa 
ifdd 
(dd 
!dd 	
routedd	 
.dd 
TryGetPropertydd 
(dd 
$strdd '
,dd' (
outdd) ,
vardd- 0
summarydd1 8
)dd8 9
)dd9 :
{ee 
throwff 
newff 
	Exceptionff 
(ff 
$strff R
)ffR S
;ffS T
}gg 
ifjj 
(jj 
!jj 	
routejj	 
.jj 
TryGetPropertyjj 
(jj 
$strjj (
,jj( )
outjj* -
varjj. 1
geometryjj2 :
)jj: ;
)jj; <
{kk 
throwll 
newll 
	Exceptionll 
(ll 
$strll T
)llT U
;llU V
}mm 
varnn 
instructionsnn 
=nn 
newnn 
Listnn 
<nn  
RouteInstructionDtonn  3
>nn3 4
(nn4 5
)nn5 6
;nn6 7
ifoo 
(oo 
routeoo 
.oo 
TryGetPropertyoo 
(oo 
$stroo '
,oo' (
outoo) ,
varoo- 0
segmentsoo1 9
)oo9 :
)oo: ;
{pp 
foreachqq 
(qq 
varqq 
segmentqq 
inqq 
segmentsqq &
.qq& '
EnumerateArrayqq' 5
(qq5 6
)qq6 7
)qq7 8
{rr 
ifss 

(ss 
segmentss 
.ss 
TryGetPropertyss "
(ss" #
$strss# *
,ss* +
outss, /
varss0 3
stepsss4 9
)ss9 :
)ss: ;
{tt 	
foreachuu
 
(uu 
varuu 
stepuu 
inuu 
stepsuu $
.uu$ %
EnumerateArrayuu% 3
(uu3 4
)uu4 5
)uu5 6
{vv
 
varww 
instructionww 
=ww 
stepww "
.ww" #
TryGetPropertyww# 1
(ww1 2
$strww2 ?
,ww? @
outwwA D
varwwE H
instrwwI N
)wwN O
?xx 
instrxx 
.xx 
	GetStringxx !
(xx! "
)xx" #
:xx$ %
$strxx& <
;xx< =
varyy 
stepDistanceyy 
=yy 
stepyy #
.yy# $
TryGetPropertyyy$ 2
(yy2 3
$stryy3 =
,yy= >
outyy? B
varyyC F
distyyG K
)yyK L
?zz 
distzz 
.zz 
	GetDoublezz  
(zz  !
)zz! "
:zz# $
$numzz% (
;zz( )
var{{ 

travelMode{{ 
={{ 
profile{{ $
.{{$ %
Split{{% *
({{* +
$char{{+ .
){{. /
[{{/ 0
$num{{0 1
]{{1 2
;{{2 3
instructions}} 
.}} 
Add}} 
(}} 
new}}  
RouteInstructionDto}}! 4
{~~ 
Instruction 
= 
instruction '
,' (
Distance
ÄÄ 
=
ÄÄ 
stepDistance
ÄÄ %
,
ÄÄ% &
Mode
ÅÅ 
=
ÅÅ 

travelMode
ÅÅ 
}
ÇÇ 
)
ÇÇ 
;
ÇÇ 
}
ÉÉ
 
}
ÑÑ 	
}
ÖÖ 
}
ÜÜ 
var
áá 
polyline
áá 
=
áá 
geometry
áá 
.
áá 
	GetString
áá %
(
áá% &
)
áá& '
;
áá' (
var
àà 
coords
àà 
=
àà "
DecodeGooglePolyline
àà %
(
àà% &
polyline
àà& .
)
àà. /
;
àà/ 0
return
ää 

new
ää 
RouteResponseDto
ää 
{
ãã 
Distance
åå 
=
åå 
summary
åå 
.
åå 
GetProperty
åå $
(
åå$ %
$str
åå% /
)
åå/ 0
.
åå0 1
	GetDouble
åå1 :
(
åå: ;
)
åå; <
,
åå< =
Duration
çç 
=
çç 
summary
çç 
.
çç 
GetProperty
çç $
(
çç$ %
$str
çç% /
)
çç/ 0
.
çç0 1
	GetDouble
çç1 :
(
çç: ;
)
çç; <
,
çç< =
Geometry
éé 
=
éé 
coords
éé 
,
éé 
Instructions
èè 
=
èè 
instructions
èè !
}
êê 
;
êê 
}
ëë 
public
ìì 
async
ìì	 
Task
ìì 
<
ìì 
RouteResponseDto
ìì $
>
ìì$ %(
CalcularConGoogleMapsAsync
ìì& @
(
ìì@ A
RouteRequestDto
ììA P
request
ììQ X
)
ììX Y
{
îî 
var
ïï 
apiKey
ïï 
=
ïï 
_config
ïï 
[
ïï 
$str
ïï 4
]
ïï4 5
;
ïï5 6
string
ññ 

origin
ññ 
=
ññ 
$"
ññ 
{
ññ 
request
ññ 
.
ññ 
	OriginLat
ññ (
.
ññ( )
ToString
ññ) 1
(
ññ1 2
System
ññ2 8
.
ññ8 9
Globalization
ññ9 F
.
ññF G
CultureInfo
ññG R
.
ññR S
InvariantCulture
ññS c
)
ññc d
}
ññd e
$str
ññe f
{
ññf g
request
ññg n
.
ññn o
	OriginLng
ñño x
.
ññx y
ToStringññy Å
(ññÅ Ç
SystemññÇ à
.ññà â
Globalizationññâ ñ
.ñññ ó
CultureInfoññó ¢
.ññ¢ £ 
InvariantCultureññ£ ≥
)ññ≥ ¥
}ññ¥ µ
"ññµ ∂
;ññ∂ ∑
string
óó 

destination
óó 
=
óó 
$"
óó 
{
óó 
request
óó #
.
óó# $
DestinationLat
óó$ 2
.
óó2 3
ToString
óó3 ;
(
óó; <
System
óó< B
.
óóB C
Globalization
óóC P
.
óóP Q
CultureInfo
óóQ \
.
óó\ ]
InvariantCulture
óó] m
)
óóm n
}
óón o
$str
óóo p
{
óóp q
request
óóq x
.
óóx y
DestinationLngóóy á
.óóá à
ToStringóóà ê
(óóê ë
Systemóóë ó
.óóó ò
Globalizationóóò •
.óó• ¶
CultureInfoóó¶ ±
.óó± ≤ 
InvariantCultureóó≤ ¬
)óó¬ √
}óó√ ƒ
"óóƒ ≈
;óó≈ ∆
var
òò 
queryParams
òò 
=
òò 
new
òò 
List
òò 
<
òò 
string
òò %
>
òò% &
{
ôô 	
$"
öö 
$str
öö 
{
öö 
Uri
öö 
.
öö 
EscapeDataString
öö *
(
öö* +
origin
öö+ 1
)
öö1 2
}
öö2 3
"
öö3 4
,
öö4 5
$"
õõ 
$str
õõ 
{
õõ 
Uri
õõ 
.
õõ 
EscapeDataString
õõ /
(
õõ/ 0
destination
õõ0 ;
)
õõ; <
}
õõ< =
"
õõ= >
,
õõ> ?
$str
úú 
,
úú 
$str
ùù  
,
ùù  !
$str
ûû 8
,
ûû8 9
$str
üü 
,
üü  
}
†† 	
;
††	 

if
¢¢ 
(
¢¢ 
request
¢¢ 
.
¢¢ 

Preference
¢¢ 
==
¢¢ 
$str
¢¢ $
)
¢¢$ %
{
££ 
queryParams
§§ 
.
§§ 
Add
§§ 
(
§§ 
$str
§§ )
)
§§) *
;
§§* +
}
•• 
else
ßß 
if
ßß	 
(
ßß 
request
ßß 
.
ßß 

Preference
ßß 
==
ßß  "
$str
ßß# (
)
ßß( )
{
®® 
queryParams
©© 
.
©© 
Add
©© 
(
©© 
$str
©© (
)
©©( )
;
©©) *
}
™™ 
var
¨¨ 
url
¨¨ 
=
¨¨ 
$"
¨¨ 
$str
¨¨ E
{
¨¨E F
string
¨¨F L
.
¨¨L M
Join
¨¨M Q
(
¨¨Q R
$str
¨¨R U
,
¨¨U V
queryParams
¨¨W b
)
¨¨b c
}
¨¨c d
$str
¨¨d i
{
¨¨i j
apiKey
¨¨j p
}
¨¨p q
"
¨¨q r
;
¨¨r s
var
≠≠ 
res
≠≠ 
=
≠≠ 
await
≠≠ 
_httpClient
≠≠ 
.
≠≠  
GetAsync
≠≠  (
(
≠≠( )
url
≠≠) ,
)
≠≠, -
.
≠≠- .
ConfigureAwait
≠≠. <
(
≠≠< =
false
≠≠= B
)
≠≠B C
;
≠≠C D
if
ÆÆ 
(
ÆÆ 
!
ÆÆ 	
res
ÆÆ	 
.
ÆÆ !
IsSuccessStatusCode
ÆÆ  
)
ÆÆ  !
{
ØØ 
throw
∞∞ 
new
∞∞ 
	Exception
∞∞ 
(
∞∞ 
$"
∞∞ 
$str
∞∞ A
{
∞∞A B
res
∞∞B E
.
∞∞E F

StatusCode
∞∞F P
}
∞∞P Q
"
∞∞Q R
)
∞∞R S
;
∞∞S T
}
±± 
var
≥≥ 
json
≥≥ 
=
≥≥ 
await
≥≥ 
res
≥≥ 
.
≥≥ 
Content
≥≥  
.
≥≥  !
ReadAsStringAsync
≥≥! 2
(
≥≥2 3
)
≥≥3 4
.
≥≥4 5
ConfigureAwait
≥≥5 C
(
≥≥C D
false
≥≥D I
)
≥≥I J
;
≥≥J K
using
µµ 	
var
µµ
 
doc
µµ 
=
µµ 
JsonDocument
µµ  
.
µµ  !
Parse
µµ! &
(
µµ& '
json
µµ' +
)
µµ+ ,
;
µµ, -
if
∂∂ 
(
∂∂ 
!
∂∂ 	
doc
∂∂	 
.
∂∂ 
RootElement
∂∂ 
.
∂∂ 
TryGetProperty
∂∂ '
(
∂∂' (
$str
∂∂( 0
,
∂∂0 1
out
∂∂2 5
var
∂∂6 9
status
∂∂: @
)
∂∂@ A
||
∂∂B D
status
∂∂E K
.
∂∂K L
	GetString
∂∂L U
(
∂∂U V
)
∂∂V W
!=
∂∂X Z
$str
∂∂[ _
)
∂∂_ `
{
∑∑ 
throw
∏∏ 
new
∏∏ 
	Exception
∏∏ 
(
∏∏ 
$"
∏∏ 
$str
∏∏ B
{
∏∏B C
doc
∏∏C F
.
∏∏F G
RootElement
∏∏G R
.
∏∏R S
GetProperty
∏∏S ^
(
∏∏^ _
$str
∏∏_ g
)
∏∏g h
.
∏∏h i
	GetString
∏∏i r
(
∏∏r s
)
∏∏s t
}
∏∏t u
"
∏∏u v
)
∏∏v w
;
∏∏w x
}
ππ 
if
ªª 
(
ªª 
!
ªª 	
doc
ªª	 
.
ªª 
RootElement
ªª 
.
ªª 
TryGetProperty
ªª '
(
ªª' (
$str
ªª( 0
,
ªª0 1
out
ªª2 5
var
ªª6 9
routes
ªª: @
)
ªª@ A
||
ªªB D
routes
ªªE K
.
ªªK L
GetArrayLength
ªªL Z
(
ªªZ [
)
ªª[ \
==
ªª] _
$num
ªª` a
)
ªªa b
{
ºº 
throw
ΩΩ 
new
ΩΩ 
	Exception
ΩΩ 
(
ΩΩ 
$str
ΩΩ i
)
ΩΩi j
;
ΩΩj k
}
ææ 
var
¡¡ 
route
¡¡ 
=
¡¡ 
routes
¡¡ 
[
¡¡ 
$num
¡¡ 
]
¡¡ 
;
¡¡ 
if
¬¬ 
(
¬¬ 
!
¬¬ 	
route
¬¬	 
.
¬¬ 
TryGetProperty
¬¬ 
(
¬¬ 
$str
¬¬ $
,
¬¬$ %
out
¬¬& )
var
¬¬* -
legs
¬¬. 2
)
¬¬2 3
||
¬¬4 6
legs
¬¬7 ;
.
¬¬; <
GetArrayLength
¬¬< J
(
¬¬J K
)
¬¬K L
==
¬¬M O
$num
¬¬P Q
)
¬¬Q R
{
√√ 
throw
ƒƒ 
new
ƒƒ 
	Exception
ƒƒ 
(
ƒƒ 
$str
ƒƒ ?
)
ƒƒ? @
;
ƒƒ@ A
}
≈≈ 
var
«« 
leg
«« 
=
«« 
legs
«« 
[
«« 
$num
«« 
]
«« 
;
«« 
if
»» 
(
»» 
!
»» 	
leg
»»	 
.
»» 
TryGetProperty
»» 
(
»» 
$str
»» &
,
»»& '
out
»»( +
var
»», /
distance
»»0 8
)
»»8 9
||
»»: <
!
»»= >
leg
»»> A
.
»»A B
TryGetProperty
»»B P
(
»»P Q
$str
»»Q [
,
»»[ \
out
»»] `
var
»»a d
duration
»»e m
)
»»m n
)
»»n o
{
…… 
throw
   
new
   
	Exception
   
(
   
$str
   T
)
  T U
;
  U V
}
ÀÀ 
if
ÕÕ 
(
ÕÕ 
!
ÕÕ 	
route
ÕÕ	 
.
ÕÕ 
TryGetProperty
ÕÕ 
(
ÕÕ 
$str
ÕÕ 1
,
ÕÕ1 2
out
ÕÕ3 6
var
ÕÕ7 :
polylineElement
ÕÕ; J
)
ÕÕJ K
||
ÕÕL N
!
ŒŒ 	
polylineElement
ŒŒ	 
.
ŒŒ 
TryGetProperty
ŒŒ '
(
ŒŒ' (
$str
ŒŒ( 0
,
ŒŒ0 1
out
ŒŒ2 5
var
ŒŒ6 9
points
ŒŒ: @
)
ŒŒ@ A
)
ŒŒA B
{
œœ 
throw
–– 
new
–– 
	Exception
–– 
(
–– 
$str
–– E
)
––E F
;
––F G
}
—— 
var
““ 
polyline
““ 
=
““ 
points
““ 
.
““ 
	GetString
““ #
(
““# $
)
““$ %
;
““% &
if
”” 
(
”” 
string
”” 
.
”” 
IsNullOrEmpty
”” 
(
”” 
polyline
”” %
)
””% &
)
””& '
{
‘‘ 
throw
’’ 
new
’’ 
	Exception
’’ 
(
’’ 
$str
’’ ?
)
’’? @
;
’’@ A
}
÷÷ 
var
◊◊ 
instructions
◊◊ 
=
◊◊ 
new
◊◊ 
List
◊◊ 
<
◊◊  !
RouteInstructionDto
◊◊  3
>
◊◊3 4
(
◊◊4 5
)
◊◊5 6
;
◊◊6 7
if
ÿÿ 
(
ÿÿ 
leg
ÿÿ 
.
ÿÿ 
TryGetProperty
ÿÿ 
(
ÿÿ 
$str
ÿÿ "
,
ÿÿ" #
out
ÿÿ$ '
var
ÿÿ( +
steps
ÿÿ, 1
)
ÿÿ1 2
)
ÿÿ2 3
{
ŸŸ 
foreach
⁄⁄ 
(
⁄⁄ 
var
⁄⁄ 
step
⁄⁄ 
in
⁄⁄ 
steps
⁄⁄  
.
⁄⁄  !
EnumerateArray
⁄⁄! /
(
⁄⁄/ 0
)
⁄⁄0 1
)
⁄⁄1 2
{
€€ 
var
‹‹ 
instruction
‹‹ 
=
‹‹ 
step
‹‹ 
.
‹‹ 
TryGetProperty
‹‹ -
(
‹‹- .
$str
‹‹. A
,
‹‹A B
out
‹‹C F
var
‹‹G J
htmlInstructions
‹‹K [
)
‹‹[ \
?
›› 
htmlInstructions
›› 
.
›› 
	GetString
›› (
(
››( )
)
››) *
:
››+ ,
$str
››- C
;
››C D
var
ﬁﬁ 
stepDistance
ﬁﬁ 
=
ﬁﬁ 
step
ﬁﬁ 
.
ﬁﬁ  
TryGetProperty
ﬁﬁ  .
(
ﬁﬁ. /
$str
ﬁﬁ/ 9
,
ﬁﬁ9 :
out
ﬁﬁ; >
var
ﬁﬁ? B
dist
ﬁﬁC G
)
ﬁﬁG H
?
ﬂﬂ 
dist
ﬂﬂ 
.
ﬂﬂ 
GetProperty
ﬂﬂ 
(
ﬂﬂ 
$str
ﬂﬂ &
)
ﬂﬂ& '
.
ﬂﬂ' (
	GetDouble
ﬂﬂ( 1
(
ﬂﬂ1 2
)
ﬂﬂ2 3
:
ﬂﬂ4 5
$num
ﬂﬂ6 9
;
ﬂﬂ9 :
var
‡‡ 

travelMode
‡‡ 
=
‡‡ 
step
‡‡ 
.
‡‡ 
TryGetProperty
‡‡ ,
(
‡‡, -
$str
‡‡- :
,
‡‡: ;
out
‡‡< ?
var
‡‡@ C
mode
‡‡D H
)
‡‡H I
?
·· 
mode
·· 
.
·· 
	GetString
·· 
(
·· 
)
·· 
.
·· 
ToLower
·· &
(
··& '
)
··' (
:
··) *
$str
··+ 4
;
··4 5
instructions
„„ 
.
„„ 
Add
„„ 
(
„„ 
new
„„ !
RouteInstructionDto
„„ 0
{
‰‰ 	
Instruction
ÂÂ
 
=
ÂÂ 
System
ÂÂ 
.
ÂÂ 
Net
ÂÂ "
.
ÂÂ" #

WebUtility
ÂÂ# -
.
ÂÂ- .

HtmlDecode
ÂÂ. 8
(
ÂÂ8 9
instruction
ÂÂ9 D
)
ÂÂD E
,
ÂÂE F
Distance
ÊÊ
 
=
ÊÊ 
stepDistance
ÊÊ !
,
ÊÊ! "
Mode
ÁÁ
 
=
ÁÁ 

travelMode
ÁÁ 
}
ËË 	
)
ËË	 

;
ËË
 
}
ÈÈ 
}
ÎÎ 
var
ÏÏ 
coords
ÏÏ 
=
ÏÏ "
DecodeGooglePolyline
ÏÏ %
(
ÏÏ% &
polyline
ÏÏ& .
)
ÏÏ. /
;
ÏÏ/ 0
return
ÓÓ 

new
ÓÓ 
RouteResponseDto
ÓÓ 
{
ÔÔ 
Distance
 
=
 
distance
 
.
 
GetProperty
 %
(
% &
$str
& -
)
- .
.
. /
	GetDouble
/ 8
(
8 9
)
9 :
,
: ;
Duration
ÒÒ 
=
ÒÒ 
duration
ÒÒ 
.
ÒÒ 
GetProperty
ÒÒ %
(
ÒÒ% &
$str
ÒÒ& -
)
ÒÒ- .
.
ÒÒ. /
	GetDouble
ÒÒ/ 8
(
ÒÒ8 9
)
ÒÒ9 :
,
ÒÒ: ;
Geometry
ÚÚ 
=
ÚÚ 
coords
ÚÚ 
,
ÚÚ 
Instructions
ÛÛ 
=
ÛÛ 
instructions
ÛÛ !
}
ÙÙ 
;
ÙÙ 
}
ıı 
private
˜˜ 	
static
˜˜
 
List
˜˜ 
<
˜˜ 
double
˜˜ 
[
˜˜ 
]
˜˜ 
>
˜˜ "
DecodeGooglePolyline
˜˜  4
(
˜˜4 5
string
˜˜5 ;
polyline
˜˜< D
)
˜˜D E
{
¯¯ 
var
˘˘ 
coords
˘˘ 
=
˘˘ 
new
˘˘ 
List
˘˘ 
<
˘˘ 
double
˘˘  
[
˘˘  !
]
˘˘! "
>
˘˘" #
(
˘˘# $
)
˘˘$ %
;
˘˘% &
int
˙˙ 
index
˙˙ 
=
˙˙ 
$num
˙˙ 
,
˙˙ 
len
˙˙ 
=
˙˙ 
polyline
˙˙ !
.
˙˙! "
Length
˙˙" (
;
˙˙( )
int
˚˚ 
lat
˚˚ 
=
˚˚ 
$num
˚˚ 
,
˚˚ 
lng
˚˚ 
=
˚˚ 
$num
˚˚ 
;
˚˚ 
while
˝˝ 	
(
˝˝
 
index
˝˝ 
<
˝˝ 
len
˝˝ 
)
˝˝ 
{
˛˛ 
int
ˇˇ 	
b
ˇˇ
 
,
ˇˇ 
shift
ˇˇ 
=
ˇˇ 
$num
ˇˇ 
,
ˇˇ 
result
ˇˇ 
=
ˇˇ  
$num
ˇˇ! "
;
ˇˇ" #
do
ÄÄ 
{
ÅÅ 
b
ÇÇ 	
=
ÇÇ
 
polyline
ÇÇ 
[
ÇÇ 
index
ÇÇ 
++
ÇÇ 
]
ÇÇ 
-
ÇÇ 
$num
ÇÇ  "
;
ÇÇ" #
result
ÉÉ 
|=
ÉÉ 
(
ÉÉ 
b
ÉÉ 
&
ÉÉ 
$num
ÉÉ 
)
ÉÉ 
<<
ÉÉ 
shift
ÉÉ  %
;
ÉÉ% &
shift
ÑÑ 
+=
ÑÑ 
$num
ÑÑ 
;
ÑÑ 
}
ÖÖ 
while
ÖÖ 
(
ÖÖ 
b
ÖÖ 
>=
ÖÖ 
$num
ÖÖ 
)
ÖÖ 
;
ÖÖ 
int
ÜÜ 	
dlat
ÜÜ
 
=
ÜÜ 
(
ÜÜ 
(
ÜÜ 
result
ÜÜ 
&
ÜÜ 
$num
ÜÜ 
)
ÜÜ 
!=
ÜÜ !
$num
ÜÜ" #
)
ÜÜ# $
?
ÜÜ% &
~
ÜÜ' (
(
ÜÜ( )
result
ÜÜ) /
>>
ÜÜ0 2
$num
ÜÜ3 4
)
ÜÜ4 5
:
ÜÜ6 7
result
ÜÜ8 >
>>
ÜÜ? A
$num
ÜÜB C
;
ÜÜC D
lat
áá 	
+=
áá
 
dlat
áá 
;
áá 
shift
ââ 
=
ââ 
$num
ââ 
;
ââ 
result
ää 
=
ää 
$num
ää 
;
ää 
do
ãã 
{
åå 
b
çç 	
=
çç
 
polyline
çç 
[
çç 
index
çç 
++
çç 
]
çç 
-
çç 
$num
çç  "
;
çç" #
result
éé 
|=
éé 
(
éé 
b
éé 
&
éé 
$num
éé 
)
éé 
<<
éé 
shift
éé  %
;
éé% &
shift
èè 
+=
èè 
$num
èè 
;
èè 
}
êê 
while
êê 
(
êê 
b
êê 
>=
êê 
$num
êê 
)
êê 
;
êê 
int
ëë 	
dlng
ëë
 
=
ëë 
(
ëë 
(
ëë 
result
ëë 
&
ëë 
$num
ëë 
)
ëë 
!=
ëë !
$num
ëë" #
)
ëë# $
?
ëë% &
~
ëë' (
(
ëë( )
result
ëë) /
>>
ëë0 2
$num
ëë3 4
)
ëë4 5
:
ëë6 7
result
ëë8 >
>>
ëë? A
$num
ëëB C
;
ëëC D
lng
íí 	
+=
íí
 
dlng
íí 
;
íí 
coords
îî 
.
îî 
Add
îî 
(
îî 
new
îî 
double
îî 
[
îî 
]
îî 
{
îî 
lng
îî  #
/
îî$ %
$num
îî& )
,
îî) *
lat
îî+ .
/
îî/ 0
$num
îî1 4
}
îî5 6
)
îî6 7
;
îî7 8
}
ïï 
return
óó 

coords
óó 
;
óó 
}
òò 
}ôô ¢

[/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/Interface/IUserService.cs
	namespace 	
Services
 
. 
	Interface 
; 
public 
	interface 
IUserService 
{ 
List		 
<		 
UserDto		 
>		 
GetAllUsers		 
(		 
)		 
;		 
bool

 

CreateUser

 
(

 

UserCreate

 
user

 !
)

! "
;

" #
Task 
< 
bool 
> 

DeleteUser 
( 
UserCredentials '
userCredentials( 7
)7 8
;8 9
Task 
< 
UserDto 
> 
Authenticate 
( 
UserCredentials ,
userCredentials- <
)< =
;= >
Task 
< 
bool 
> 

ModifyUser 
( 
UserDto 

userModify  *
)* +
;+ ,
Task 
< 
UserDto 
>  
LoginWithGoogleAsync $
($ %
LoginGoogleDto% 3
dto4 7
)7 8
;8 9
} €
`/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/Interface/IUbicationService.cs
	namespace 	
	plantilla
 
. 
Web 
. 
src 
. 
Services $
.$ %
	Interface% .
;. /
public 
	interface 
IUbicationService "
{		 
Task

 
<

 
List

 
<

 
SavedUbicationDto

 
>

 
>

 &
GetUbicationsByUserIdAsync

  :
(

: ;
string

; A
	userEmail

B K
)

K L
;

L M
Task 
< 
bool 
> 
SaveUbicationAsync 
(  
SavedUbicationDto  1
savedUbication2 @
)@ A
;A B
Task 
< 
bool 
> 
DeleteUbication 
( 
UbicationInfoDto -
ubicationDeleteDto. @
)@ A
;A B
Task 
< 
bool 
> 
UpdateUbication 
( 
UbicationInfoDto -
savedUbication. <
)< =
;= >
Task 
< 
Object 
> 
GetUbicationDetails "
(" #
int# &
ubicationId' 2
,2 3
string4 :
stationType; F
)F G
;G H
} ó
Z/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/Interface/ITmbService.cs
	namespace 	
Services
 
. 
	Interface 
; 
public 
	interface 
ITmbService 
{ 
Task		 
<		 
List		 
<		 
MetroDto		 
>		 
>		 
GetAllMetrosAsync		 (
(		( )
)		) *
;		* +
Task

 
<

 
List

 
<

 
BusDto

 
>

 
>

 
GetAllBusAsync

 #
(

# $
)

$ %
;

% &
Task 
< 
MetroDto 
? 
> 
GetMetroByIdAsync #
(# $
int$ '
id( *
)* +
;+ ,
Task 
< 
BusDto 
? 
> 
GetBusByIdAsync 
(  
int  #
id$ &
)& '
;' (
} Ô
b/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/Interface/IStateBicingService.cs
	namespace 	
Services
 
. 
	Interface 
; 
public 
	interface 
IStateBicingService $
{ 
Task		 
<		 
List		 
<		 
StateBicingDto		 
>		 
>		 *
GetAllStateBicingStationsAsync		 ;
(		; <
)		< =
;		= >
Task

 1
%FetchAndStoreStateBicingStationsAsync

 ,
(

, -
)

- .
;

. /
} ‘
\/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/Interface/IRouteService.cs
public 
	interface 
IRouteService 
{ 
public 
Task	 
< 
RouteResponseDto 
> 
CalcularRutaAsync  1
(1 2
RouteRequestDto2 A
requestB I
,I J
GuidK O
	usuarioIdP Y
)Y Z
;Z [
}		 ˜
g/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/Interface/IChargingStationsService.cs
	namespace 	
Services
 
. 
	Interface 
; 
public 
	interface $
IChargingStationsService )
{ 
Task		 
<		 
List		 
<		 
ChargingStationDto		 
>		 
>		  '
GetAllChargingStationsAsync		! <
(		< =
)		= >
;		> ?
Task .
"FetchAndStoreChargingStationsAsync )
() *
)* +
;+ ,
} Î
d/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/Interface/IBicingStationService.cs
	namespace 	
Services
 
. 
	Interface 
; 
public 
	interface !
IBicingStationService &
{ 
Task		 
<		 
List		 
<		 
BicingStationDto		 
>		 
>		 %
GetAllBicingStationsAsync		 8
(		8 9
)		9 :
;		: ;
Task

 ,
 FetchAndStoreBicingStationsAsync

 '
(

' (
)

( )
;

) *
} „¥
\/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/ChargingStationsService.cs
	namespace 	
Services
 
; 
public 
class #
ChargingStationsService $
:% &$
IChargingStationsService' ?
{ 
private 	
readonly
 '
IChargingStationsRepository .'
_chargingStationsRepository/ J
;J K
private 	
readonly
 

HttpClient 
_httpClient )
;) *
private 	
readonly
 
ILogger 
< #
ChargingStationsService 2
>2 3
_logger4 ;
;; <
private 	
readonly
 
string 
	_apiToken #
=$ %
$str& h
;h i
private 	
JsonElement
 
? 
_cachedJsonData &
;& '
private 	
DateTime
 
_lastFetchTime !
=" #
DateTime$ ,
., -
MinValue- 5
;5 6
private 	
readonly
 
TimeSpan 
_cacheDuration *
=+ ,
TimeSpan- 5
.5 6
FromMinutes6 A
(A B
$numB D
)D E
;E F
public #
ChargingStationsService	  
(  !

HttpClient! +

httpClient, 6
,6 7
ILogger8 ?
<? @#
ChargingStationsService@ W
>W X
loggerY _
,_ `'
IChargingStationsRepositorya |'
chargingStationsRepository	} ó
)
ó ò
{ 
_httpClient 
= 

httpClient 
; 
_logger 
= 
logger 
; '
_chargingStationsRepository 
=  !&
chargingStationsRepository" <
;< =
}   
public"" 
async""	 
Task"" 
<"" 
List"" 
<"" 
ChargingStationDto"" +
>""+ ,
>"", -'
GetAllChargingStationsAsync"". I
(""I J
)""J K
{## 
return$$ 

await$$ '
_chargingStationsRepository$$ ,
.$$, -"
GetAllChargingStations$$- C
($$C D
)$$D E
.$$E F
ConfigureAwait$$F T
($$T U
false$$U Z
)$$Z [
;$$[ \
}%% 
public'' 
async''	 
Task'' .
"FetchAndStoreChargingStationsAsync'' 6
(''6 7
)''7 8
{(( 
try)) 
{** 
if++ 
(++	 

_cachedJsonData++
 
==++ 
null++ !
||++" $
DateTime++% -
.++- .
Now++. 1
-++2 3
_lastFetchTime++4 B
>++C D
_cacheDuration++E S
)++S T
{,, 
var-- 

requestUrl-- 
=-- 
$str	-- ≠
;
--≠ Æ
_httpClient.. 
... !
DefaultRequestHeaders.. )
...) *
Authorization..* 7
=..8 9
new..: =%
AuthenticationHeaderValue..> W
(..W X
$str..X `
,..` a
	_apiToken..b k
)..k l
;..l m
var00 
response00 
=00 
await00 
_httpClient00 (
.00( )
GetAsync00) 1
(001 2

requestUrl002 <
)00< =
.00= >
ConfigureAwait00> L
(00L M
false00M R
)00R S
;00S T
response11 
.11 #
EnsureSuccessStatusCode11 (
(11( )
)11) *
;11* +
var33 
jsonResponse33 
=33 
await33  
response33! )
.33) *
Content33* 1
.331 2
ReadAsStringAsync332 C
(33C D
)33D E
.33E F
ConfigureAwait33F T
(33T U
false33U Z
)33Z [
;33[ \
_cachedJsonData55 
=55 
JsonSerializer55 (
.55( )
Deserialize55) 4
<554 5
JsonElement555 @
>55@ A
(55A B
jsonResponse55B N
)55N O
;55O P
_lastFetchTime66 
=66 
DateTime66 !
.66! "
Now66" %
;66% &
}77 
var99 	
locationsToAdd99
 
=99 
new99 
List99 #
<99# $
LocationEntity99$ 2
>992 3
(993 4
)994 5
;995 6
var:: 	

hostsToAdd::
 
=:: 
new:: 
List:: 
<::  

HostEntity::  *
>::* +
(::+ ,
)::, -
;::- .
var;; 	
stationsToAdd;;
 
=;; 
new;; 
List;; "
<;;" #
StationEntity;;# 0
>;;0 1
(;;1 2
);;2 3
;;;3 4
var<< 	

portsToAdd<<
 
=<< 
new<< 
List<< 
<<<  

PortEntity<<  *
><<* +
(<<+ ,
)<<, -
;<<- .
if>> 
(>>	 

!>>
 
_cachedJsonData>> 
.>> 
HasValue>> #
||>>$ &
!>>' (
_cachedJsonData>>( 7
.>>7 8
Value>>8 =
.>>= >
TryGetProperty>>> L
(>>L M
$str>>M X
,>>X Y
out>>Z ]
var>>^ a
locationsElement>>b r
)>>r s
)>>s t
{?? 
_logger@@ 
.@@ 
LogError@@ 
(@@ 
$str@@ b
,@@b c
_cachedJsonData@@d s
)@@s t
;@@t u
returnAA 
;AA 
}BB 
foreachDD 
(DD 
varDD 
locationElementDD "
inDD# %
locationsElementDD& 6
.DD6 7
EnumerateArrayDD7 E
(DDE F
)DDF G
)DDG H
{EE 
varFF 

locationIdFF 
=FF 
locationElementFF (
.FF( )
GetPropertyFF) 4
(FF4 5
$strFF5 9
)FF9 :
.FF: ;
	GetStringFF; D
(FFD E
)FFE F
;FFF G
locationsToAddGG 
.GG 
AddGG 
(GG 
newGG 
LocationEntityGG -
{HH 	

LocationIdII
 
=II 

locationIdII !
,II! "
NetworkBrandNameJJ
 
=JJ 
locationElementJJ ,
.JJ, -
GetPropertyJJ- 8
(JJ8 9
$strJJ9 M
)JJM N
.JJN O
	GetStringJJO X
(JJX Y
)JJY Z
,JJZ [
OperatorPhoneKK
 
=KK 
locationElementKK )
.KK) *
GetPropertyKK* 5
(KK5 6
$strKK6 ?
)KK? @
.KK@ A
GetPropertyKKA L
(KKL M
$strKKM ]
)KK] ^
.KK^ _
	GetStringKK_ h
(KKh i
)KKi j
,KKj k
OperatorWebsiteLL
 
=LL 
locationElementLL +
.LL+ ,
GetPropertyLL, 7
(LL7 8
$strLL8 A
)LLA B
.LLB C
GetPropertyLLC N
(LLN O
$strLLO a
)LLa b
.LLb c
	GetStringLLc l
(LLl m
)LLm n
,LLn o
LatitudeMM
 
=MM 
locationElementMM $
.MM$ %
GetPropertyMM% 0
(MM0 1
$strMM1 >
)MM> ?
.MM? @
GetPropertyMM@ K
(MMK L
$strMML V
)MMV W
.MMW X
	GetDoubleMMX a
(MMa b
)MMb c
,MMc d
	LongitudeNN
 
=NN 
locationElementNN %
.NN% &
GetPropertyNN& 1
(NN1 2
$strNN2 ?
)NN? @
.NN@ A
GetPropertyNNA L
(NNL M
$strNNM X
)NNX Y
.NNY Z
	GetDoubleNNZ c
(NNc d
)NNd e
,NNe f
AddressStringOO
 
=OO 
locationElementOO )
.OO) *
GetPropertyOO* 5
(OO5 6
$strOO6 ?
)OO? @
.OO@ A
GetPropertyOOA L
(OOL M
$strOOM ]
)OO] ^
.OO^ _
	GetStringOO_ h
(OOh i
)OOi j
,OOj k
LocalityPP
 
=PP 
locationElementPP $
.PP$ %
GetPropertyPP% 0
(PP0 1
$strPP1 :
)PP: ;
.PP; <
GetPropertyPP< G
(PPG H
$strPPH R
)PPR S
.PPS T
	GetStringPPT ]
(PP] ^
)PP^ _
,PP_ `

PostalCodeQQ
 
=QQ 
locationElementQQ &
.QQ& '
GetPropertyQQ' 2
(QQ2 3
$strQQ3 <
)QQ< =
.QQ= >
GetPropertyQQ> I
(QQI J
$strQQJ W
)QQW X
.QQX Y
	GetStringQQY b
(QQb c
)QQc d
}RR 	
)RR	 

;RR
 
ifTT 

(TT 
locationElementTT 
.TT 
TryGetPropertyTT *
(TT* +
$strTT+ 1
,TT1 2
outTT3 6
varTT7 :
hostElementTT; F
)TTF G
)TTG H
{UU 	

hostsToAddVV
 
.VV 
AddVV 
(VV 
newVV 

HostEntityVV '
{WW
 
HostIdXX 
=XX 
GuidXX 
.XX 
NewGuidXX !
(XX! "
)XX" #
,XX# $
HostNameYY 
=YY 
hostElementYY "
.YY" #
GetPropertyYY# .
(YY. /
$strYY/ 5
)YY5 6
.YY6 7
	GetStringYY7 @
(YY@ A
)YYA B
,YYB C
HostAddressZZ 
=ZZ 
hostElementZZ %
.ZZ% &
GetPropertyZZ& 1
(ZZ1 2
$strZZ2 ;
)ZZ; <
.ZZ< =
GetPropertyZZ= H
(ZZH I
$strZZI Y
)ZZY Z
.ZZZ [
	GetStringZZ[ d
(ZZd e
)ZZe f
,ZZf g
HostLocality[[ 
=[[ 
hostElement[[ &
.[[& '
GetProperty[[' 2
([[2 3
$str[[3 <
)[[< =
.[[= >
GetProperty[[> I
([[I J
$str[[J T
)[[T U
.[[U V
	GetString[[V _
([[_ `
)[[` a
,[[a b
HostPostalCode\\ 
=\\ 
hostElement\\ (
.\\( )
GetProperty\\) 4
(\\4 5
$str\\5 >
)\\> ?
.\\? @
GetProperty\\@ K
(\\K L
$str\\L Y
)\\Y Z
.\\Z [
	GetString\\[ d
(\\d e
)\\e f
,\\f g
OperatorPhone]] 
=]] 
hostElement]] '
.]]' (
GetProperty]]( 3
(]]3 4
$str]]4 =
)]]= >
.]]> ?
GetProperty]]? J
(]]J K
$str]]K [
)]][ \
.]]\ ]
	GetString]]] f
(]]f g
)]]g h
,]]h i
OperatorWebsite^^ 
=^^ 
hostElement^^ )
.^^) *
GetProperty^^* 5
(^^5 6
$str^^6 ?
)^^? @
.^^@ A
GetProperty^^A L
(^^L M
$str^^M _
)^^_ `
.^^` a
	GetString^^a j
(^^j k
)^^k l
,^^l m

LocationId__ 
=__ 

locationId__ #
}``
 
)`` 
;`` 
}aa 	
ifcc 

(cc 
locationElementcc 
.cc 
TryGetPropertycc *
(cc* +
$strcc+ 5
,cc5 6
outcc7 :
varcc; >
stationsElementcc? N
)ccN O
)ccO P
{dd 	
foreachee
 
(ee 
varee 
stationElementee %
inee& (
stationsElementee) 8
.ee8 9
EnumerateArrayee9 G
(eeG H
)eeH I
)eeI J
{ff
 
vargg 
	stationIdgg 
=gg 
stationElementgg *
.gg* +
GetPropertygg+ 6
(gg6 7
$strgg7 ;
)gg; <
.gg< =
	GetStringgg= F
(ggF G
)ggG H
;ggH I
stringii 
stationLabelii 
=ii  !
nullii" &
;ii& '
ifjj 
(jj 
stationElementjj 
.jj 
TryGetPropertyjj -
(jj- .
$strjj. 5
,jj5 6
outjj7 :
varjj; >
labelElementjj? K
)jjK L
)jjL M
{kk 
stationLabelll 
=ll 
labelElementll )
.ll) *
	GetStringll* 3
(ll3 4
)ll4 5
;ll5 6
}mm 
elsenn 
{oo 
_loggerpp 
.pp 

LogWarningpp  
(pp  !
$strpp! z
,ppz {
	stationId	pp| Ö
)
ppÖ Ü
;
ppÜ á
}qq 
floatss 
stationLatitudess !
=ss" #
$numss$ %
;ss% &
floattt 
stationLongitudett "
=tt# $
$numtt% &
;tt& '
ifvv 
(vv 
stationElementvv 
.vv 
TryGetPropertyvv -
(vv- .
$strvv. ;
,vv; <
outvv= @
varvvA D
coordinatesElementvvE W
)vvW X
)vvX Y
{ww 
ifxx 
(xx 
coordinatesElementxx $
.xx$ %
TryGetPropertyxx% 3
(xx3 4
$strxx4 >
,xx> ?
outxx@ C
varxxD G

latElementxxH R
)xxR S
)xxS T
{yy 
stationLatitudezz 
=zz  !
(zz" #
floatzz# (
)zz( )

latElementzz) 3
.zz3 4
	GetDoublezz4 =
(zz= >
)zz> ?
;zz? @
}{{ 
if|| 
(|| 
coordinatesElement|| $
.||$ %
TryGetProperty||% 3
(||3 4
$str||4 ?
,||? @
out||A D
var||E H

lonElement||I S
)||S T
)||T U
{}} 
stationLongitude~~  
=~~! "
(~~# $
float~~$ )
)~~) *

lonElement~~* 4
.~~4 5
	GetDouble~~5 >
(~~> ?
)~~? @
;~~@ A
} 
}
ÄÄ 
else
ÅÅ 
{
ÇÇ 
_logger
ÉÉ 
.
ÉÉ 

LogWarning
ÉÉ  
(
ÉÉ  !
$str
ÉÉ! f
,
ÉÉf g
	stationId
ÉÉh q
)
ÉÉq r
;
ÉÉr s
}
ÑÑ 
stationsToAdd
ÜÜ 
.
ÜÜ 
Add
ÜÜ 
(
ÜÜ 
new
ÜÜ !
StationEntity
ÜÜ" /
{
áá 
	StationId
àà 
=
àà 
	stationId
àà #
,
àà# $
StationLabel
ââ 
=
ââ 
stationLabel
ââ )
,
ââ) *
StationLatitude
ää 
=
ää 
stationLatitude
ää  /
,
ää/ 0
StationLongitude
ãã 
=
ãã  
stationLongitude
ãã! 1
,
ãã1 2

LocationId
åå 
=
åå 

locationId
åå %
}
çç 
)
çç 
;
çç 
if
èè 
(
èè 
stationElement
èè 
.
èè 
TryGetProperty
èè -
(
èè- .
$str
èè. 5
,
èè5 6
out
èè7 :
var
èè; >
portsElement
èè? K
)
èèK L
)
èèL M
{
êê 
foreach
ëë 
(
ëë 
var
ëë 
portElement
ëë &
in
ëë' )
portsElement
ëë* 6
.
ëë6 7
EnumerateArray
ëë7 E
(
ëëE F
)
ëëF G
)
ëëG H
{
íí 
string
ìì 
portId
ìì 
=
ìì 
portElement
ìì  +
.
ìì+ ,
TryGetProperty
ìì, :
(
ìì: ;
$str
ìì; ?
,
ìì? @
out
ììA D
var
ììE H
	idElement
ììI R
)
ììR S
?
ììT U
	idElement
îî 
.
îî 
	GetString
îî '
(
îî' (
)
îî( )
:
îî* +
$str
îî, 3
+
îî4 5
Guid
îî6 :
.
îî: ;
NewGuid
îî; B
(
îîB C
)
îîC D
.
îîD E
ToString
îîE M
(
îîM N
)
îîN O
;
îîO P
string
ññ 
connectorType
ññ $
=
ññ% &
$str
ññ' 0
;
ññ0 1
if
óó 
(
óó 
portElement
óó 
.
óó  
TryGetProperty
óó  .
(
óó. /
$str
óó/ ?
,
óó? @
out
óóA D
var
óóE H
connectorElement
óóI Y
)
óóY Z
)
óóZ [
{
òò 
connectorType
ôô 
=
ôô  !
connectorElement
ôô" 2
.
ôô2 3
	GetString
ôô3 <
(
ôô< =
)
ôô= >
;
ôô> ?
}
öö 
double
úú 
powerKw
úú 
=
úú  
$num
úú! "
;
úú" #
if
ùù 
(
ùù 
portElement
ùù 
.
ùù  
TryGetProperty
ùù  .
(
ùù. /
$str
ùù/ 9
,
ùù9 :
out
ùù; >
var
ùù? B
powerElement
ùùC O
)
ùùO P
)
ùùP Q
{
ûû 
powerKw
üü 
=
üü 
powerElement
üü (
.
üü( )
	GetDouble
üü) 2
(
üü2 3
)
üü3 4
;
üü4 5
}
†† 
string
¢¢ 
chargingMechanism
¢¢ (
=
¢¢) *
$str
¢¢+ 4
;
¢¢4 5
if
££ 
(
££ 
portElement
££ 
.
££  
TryGetProperty
££  .
(
££. /
$str
££/ C
,
££C D
out
££E H
var
££I L
mechanismElement
££M ]
)
££] ^
)
££^ _
{
§§ 
chargingMechanism
•• #
=
••$ %
mechanismElement
••& 6
.
••6 7
	GetString
••7 @
(
••@ A
)
••A B
;
••B C
}
¶¶ 
string
®® 
status
®® 
=
®® 
$str
®®  )
;
®®) *
if
©© 
(
©© 
portElement
©© 
.
©©  
TryGetProperty
©©  .
(
©©. /
$str
©©/ <
,
©©< =
out
©©> A
var
©©B E
statusArray
©©F Q
)
©©Q R
&&
©©S U
statusArray
™™ 
.
™™  
GetArrayLength
™™  .
(
™™. /
)
™™/ 0
>
™™1 2
$num
™™3 4
&&
™™5 7
statusArray
´´ 
[
´´  
$num
´´  !
]
´´! "
.
´´" #
TryGetProperty
´´# 1
(
´´1 2
$str
´´2 :
,
´´: ;
out
´´< ?
var
´´@ C
statusElement
´´D Q
)
´´Q R
)
´´R S
{
¨¨ 
status
≠≠ 
=
≠≠ 
statusElement
≠≠ (
.
≠≠( )
	GetString
≠≠) 2
(
≠≠2 3
)
≠≠3 4
;
≠≠4 5
}
ÆÆ 
bool
∞∞ 

reservable
∞∞ 
=
∞∞  !
false
∞∞" '
;
∞∞' (
if
±± 
(
±± 
portElement
±± 
.
±±  
TryGetProperty
±±  .
(
±±. /
$str
±±/ ;
,
±±; <
out
±±= @
var
±±A D
reservableElement
±±E V
)
±±V W
)
±±W X
{
≤≤ 

reservable
≥≥ 
=
≥≥ 
reservableElement
≥≥ 0
.
≥≥0 1

GetBoolean
≥≥1 ;
(
≥≥; <
)
≥≥< =
;
≥≥= >
}
¥¥ 
DateTime
∂∂ 
lastUpdated
∂∂ $
=
∂∂% &
DateTime
∂∂' /
.
∂∂/ 0
Now
∂∂0 3
;
∂∂3 4
if
∑∑ 
(
∑∑ 
portElement
∑∑ 
.
∑∑  
TryGetProperty
∑∑  .
(
∑∑. /
$str
∑∑/ =
,
∑∑= >
out
∑∑? B
var
∑∑C F 
lastUpdatedElement
∑∑G Y
)
∑∑Y Z
)
∑∑Z [
{
∏∏ 
try
ππ 
{
∫∫ 
lastUpdated
ªª 
=
ªª  !
DateTime
ªª" *
.
ªª* +
Parse
ªª+ 0
(
ªª0 1 
lastUpdatedElement
ªª1 C
.
ªªC D
	GetString
ªªD M
(
ªªM N
)
ªªN O
)
ªªO P
;
ªªP Q
}
ºº 
catch
ΩΩ 
(
ΩΩ 
FormatException
ΩΩ (
)
ΩΩ( )
{
ææ 
_logger
øø 
.
øø 

LogWarning
øø &
(
øø& '
$str
øø' g
,
øøg h
portId
øøi o
)
øøo p
;
øøp q
}
¿¿ 
}
¡¡ 

portsToAdd
√√ 
.
√√ 
Add
√√ 
(
√√ 
new
√√ "

PortEntity
√√# -
{
ƒƒ 
PortId
≈≈ 
=
≈≈ 
portId
≈≈ !
,
≈≈! "
ConnectorType
∆∆ 
=
∆∆  !
connectorType
∆∆" /
,
∆∆/ 0
PowerKw
«« 
=
«« 
powerKw
«« #
,
««# $
ChargingMechanism
»» #
=
»»$ %
chargingMechanism
»»& 7
,
»»7 8
Status
…… 
=
…… 
status
…… !
,
……! "

Reservable
   
=
   

reservable
   )
,
  ) *
LastUpdated
ÀÀ 
=
ÀÀ 
lastUpdated
ÀÀ  +
,
ÀÀ+ ,
	StationId
ÃÃ 
=
ÃÃ 
	stationId
ÃÃ '
}
ÕÕ 
)
ÕÕ 
;
ÕÕ 
}
ŒŒ 
}
œœ 
}
––
 
}
—— 	
}
““ 
await
‘‘ )
_chargingStationsRepository
‘‘ '
.
‘‘' (
BulkInsertAsync
‘‘( 7
(
‘‘7 8
locationsToAdd
‘‘8 F
,
‘‘F G

hostsToAdd
‘‘H R
,
‘‘R S
stationsToAdd
‘‘T a
,
‘‘a b

portsToAdd
‘‘c m
)
‘‘m n
.
‘‘n o
ConfigureAwait
‘‘o }
(
‘‘} ~
false‘‘~ É
)‘‘É Ñ
;‘‘Ñ Ö
_logger
’’ 
.
’’ 
LogInformation
’’ 
(
’’ 
$str
’’ 
,’’ Ä
locationsToAdd
÷÷
 
.
÷÷ 
Count
÷÷ 
,
÷÷ 

hostsToAdd
÷÷  *
.
÷÷* +
Count
÷÷+ 0
,
÷÷0 1
stationsToAdd
÷÷2 ?
.
÷÷? @
Count
÷÷@ E
,
÷÷E F

portsToAdd
÷÷G Q
.
÷÷Q R
Count
÷÷R W
)
÷÷W X
;
÷÷X Y
}
◊◊ 
catch
ÿÿ 	
(
ÿÿ
 
	Exception
ÿÿ 
ex
ÿÿ 
)
ÿÿ 
{
ŸŸ 
_logger
⁄⁄ 
.
⁄⁄ 
LogError
⁄⁄ 
(
⁄⁄ 
ex
⁄⁄ 
,
⁄⁄ 
$str
⁄⁄ U
)
⁄⁄U V
;
⁄⁄V W
throw
€€ 
;
€€ 
}
‹‹ 
}
›› 
}ﬁﬁ ˝
f/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/ChargingStationsBackgroundService.cs
	namespace		 	
Services		
 
;		 
public 
class -
!ChargingStationsBackgroundService .
:/ 0
BackgroundService1 B
{ 
private 	
readonly
 
IServiceProvider #
_serviceProvider$ 4
;4 5
private 	
readonly
 
ILogger 
< -
!ChargingStationsBackgroundService <
>< =
_logger> E
;E F
private 	
readonly
 
TimeSpan 
	_interval %
=& '
TimeSpan( 0
.0 1
FromMinutes1 <
(< =
$num= ?
)? @
;@ A
public -
!ChargingStationsBackgroundService	 *
(* +
IServiceProvider 
serviceProvider &
,& '
ILogger 
< -
!ChargingStationsBackgroundService /
>/ 0
logger1 7
)7 8
{ 
_serviceProvider 
= 
serviceProvider &
;& '
_logger 
= 
logger 
; 
} 
	protected 
override 
async 
Task 
ExecuteAsync  ,
(, -
CancellationToken- >
stoppingToken? L
)L M
{ 
while 	
(
 
! 
stoppingToken 
. #
IsCancellationRequested 1
)1 2
{ 
try 	
{ 
_logger 
. 
LogInformation 
( 
$str N
)N O
;O P
using"" 
("" 
var"" 
scope"" 
="" 
_serviceProvider"" +
.""+ ,
CreateScope"", 7
(""7 8
)""8 9
)""9 :
{## 	
var$$
 
chargingService$$ 
=$$ 
scope$$  %
.$$% &
ServiceProvider$$& 5
.$$5 6
GetRequiredService$$6 H
<$$H I$
IChargingStationsService$$I a
>$$a b
($$b c
)$$c d
;$$d e
await&&
 
chargingService&& 
.&&  .
"FetchAndStoreChargingStationsAsync&&  B
(&&B C
)&&C D
.&&D E
ConfigureAwait&&E S
(&&S T
false&&T Y
)&&Y Z
;&&Z [
}'' 	
_logger)) 
.)) 
LogInformation)) 
()) 
$str)) O
)))O P
;))P Q
}** 
catch++ 
(++ 
	Exception++ 
ex++ 
)++ 
{,, 
_logger-- 
.-- 
LogError-- 
(-- 
ex-- 
,-- 
$str-- P
)--P Q
;--Q R
}.. 
await00 
Task00 
.00 
Delay00 
(00 
	_interval00  
,00  !
stoppingToken00" /
)00/ 0
.000 1
ConfigureAwait001 ?
(00? @
false00@ E
)00E F
;00F G
}11 
}22 
}33 èâ
Y/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/BicingStationService.cs
	namespace 	
Services
 
; 
public 
class  
BicingStationService !
:" #!
IBicingStationService$ 9
{ 
private 	
readonly
 $
IBicingStationRepository +$
_bicingStationRepository, D
;D E
private 	
readonly
 

HttpClient 
_httpClient )
;) *
private 	
readonly
 
ILogger 
<  
BicingStationService /
>/ 0
_logger1 8
;8 9
private 	
readonly
 
string 
	_apiToken #
=$ %
$str& h
;h i
private 	
JsonElement
 
? 
_cachedJsonData &
;& '
private 	
DateTime
 
_lastFetchTime !
=" #
DateTime$ ,
., -
MinValue- 5
;5 6
private 	
readonly
 
TimeSpan 
_cacheDuration *
=+ ,
TimeSpan- 5
.5 6
FromMinutes6 A
(A B
$numB D
)D E
;E F
public  
BicingStationService	 
( 

HttpClient (

httpClient) 3
,3 4
ILogger5 <
<< = 
BicingStationService= Q
>Q R
loggerS Y
,Y Z$
IBicingStationRepository 4#
bicingStationRepository5 L
)L M
{ 
_httpClient 
= 

httpClient 
; 
_logger 
= 
logger 
; $
_bicingStationRepository   
=   #
bicingStationRepository   6
;  6 7
}!! 
public## 
async##	 
Task## 
<## 
List## 
<## 
BicingStationDto## )
>##) *
>##* +%
GetAllBicingStationsAsync##, E
(##E F
)##F G
{$$ 
return%% 

await%% $
_bicingStationRepository%% )
.%%) * 
GetAllBicingStations%%* >
(%%> ?
)%%? @
.%%@ A
ConfigureAwait%%A O
(%%O P
false%%P U
)%%U V
;%%V W
}&& 
public(( 
async((	 
Task(( ,
 FetchAndStoreBicingStationsAsync(( 4
(((4 5
)((5 6
{)) 
try** 
{++ 
await,, %
RefreshCacheIfNeededAsync,, %
(,,% &
),,& '
.,,' (
ConfigureAwait,,( 6
(,,6 7
false,,7 <
),,< =
;,,= >
var-- 	
bicingStationsToAdd--
 
=-- 
await--  %$
ProcessStationsDataAsync--& >
(--> ?
)--? @
.--@ A
ConfigureAwait--A O
(--O P
false--P U
)--U V
;--V W
await.. $
_bicingStationRepository.. $
...$ %
BulkInsertAsync..% 4
(..4 5
bicingStationsToAdd..5 H
)..H I
...I J
ConfigureAwait..J X
(..X Y
false..Y ^
)..^ _
;.._ `
_logger00 
.00 
LogInformation00 
(00 
$str00 F
,00F G
bicingStationsToAdd00H [
.00[ \
Count00\ a
)00a b
;00b c
}11 
catch22 	
(22
 
	Exception22 
ex22 
)22 
{33 
_logger44 
.44 
LogError44 
(44 
ex44 
,44 
$str44 T
)44T U
;44U V
throw55 
;55 
}66 
}77 
private99 	
async99
 
Task99 %
RefreshCacheIfNeededAsync99 .
(99. /
)99/ 0
{:: 
if;; 
(;; 
_cachedJsonData;; 
==;; 
null;; 
||;;  "
DateTime;;# +
.;;+ ,
Now;;, /
-;;0 1
_lastFetchTime;;2 @
>;;A B
_cacheDuration;;C Q
);;Q R
{<< 
var>> 	

requestUrl>>
 
=>> 
$str	>> ±
;
>>± ≤
_httpClient?? 
.?? !
DefaultRequestHeaders?? '
.??' (
Clear??( -
(??- .
)??. /
;??/ 0
_httpClient@@ 
.@@ !
DefaultRequestHeaders@@ '
.@@' (
Accept@@( .
.@@. /
Add@@/ 2
(@@2 3
new@@3 6+
MediaTypeWithQualityHeaderValue@@7 V
(@@V W
$str@@W i
)@@i j
)@@j k
;@@k l
_httpClientAA 
.AA !
DefaultRequestHeadersAA '
.AA' (
AuthorizationAA( 5
=AA6 7
newAA8 ;%
AuthenticationHeaderValueAA< U
(AAU V
	_apiTokenAAV _
)AA_ `
;AA` a
varCC 	
responseCC
 
=CC 
awaitCC 
_httpClientCC &
.CC& '
GetAsyncCC' /
(CC/ 0

requestUrlCC0 :
)CC: ;
.CC; <
ConfigureAwaitCC< J
(CCJ K
falseCCK P
)CCP Q
;CCQ R
responseDD 
.DD #
EnsureSuccessStatusCodeDD &
(DD& '
)DD' (
;DD( )
varFF 	
jsonResponseFF
 
=FF 
awaitFF 
responseFF '
.FF' (
ContentFF( /
.FF/ 0
ReadAsStringAsyncFF0 A
(FFA B
)FFB C
.FFC D
ConfigureAwaitFFD R
(FFR S
falseFFS X
)FFX Y
;FFY Z
_cachedJsonDataGG 
=GG 
JsonSerializerGG &
.GG& '
DeserializeGG' 2
<GG2 3
JsonElementGG3 >
>GG> ?
(GG? @
jsonResponseGG@ L
)GGL M
;GGM N
_lastFetchTimeHH 
=HH 
DateTimeHH 
.HH  
NowHH  #
;HH# $
}II 
}JJ 
privateLL 	
asyncLL
 
TaskLL 
<LL 
ListLL 
<LL 
BicingStationEntityLL -
>LL- .
>LL. /$
ProcessStationsDataAsyncLL0 H
(LLH I
)LLI J
{MM 
ifNN 
(NN 
!NN 	
_cachedJsonDataNN	 
.NN 
HasValueNN !
||NN" $
!NN% &
_cachedJsonDataNN& 5
.NN5 6
ValueNN6 ;
.NN; <
TryGetPropertyNN< J
(NNJ K
$strNNK Q
,NNQ R
outNNS V
varNNW Z
dataElementNN[ f
)NNf g
)NNg h
{OO 
_loggerPP 
.PP 
LogErrorPP 
(PP 
$strPP G
)PPG H
;PPH I
returnQQ 
newQQ 
ListQQ 
<QQ 
BicingStationEntityQQ )
>QQ) *
(QQ* +
)QQ+ ,
;QQ, -
}RR 
varTT 
bicingStationsToAddTT 
=TT 
newTT !
ListTT" &
<TT& '
BicingStationEntityTT' :
>TT: ;
(TT; <
)TT< =
;TT= >
ifVV 
(VV 
dataElementVV 
.VV 
TryGetPropertyVV "
(VV" #
$strVV# -
,VV- .
outVV/ 2
varVV3 6
stationsElementVV7 F
)VVF G
)VVG H
{WW 
foreachXX 
(XX 
varXX 
stationElementXX !
inXX" $
stationsElementXX% 4
.XX4 5
EnumerateArrayXX5 C
(XXC D
)XXD E
)XXE F
{YY 
varZZ 
stationZZ 
=ZZ  
ProcessSingleStationZZ *
(ZZ* +
stationElementZZ+ 9
)ZZ9 :
;ZZ: ;
bicingStationsToAdd[[ 
.[[ 
Add[[ 
([[  
station[[  '
)[[' (
;[[( )
}\\ 
}]] 
return__ 

bicingStationsToAdd__ 
;__ 
}`` 
privatebb 	
BicingStationEntitybb
  
ProcessSingleStationbb 2
(bb2 3
JsonElementbb3 >
stationElementbb? M
)bbM N
{cc 
ifdd 
(dd 
!dd 	
stationElementdd	 
.dd 
TryGetPropertydd &
(dd& '
$strdd' 3
,dd3 4
outdd5 8
vardd9 <
	idElementdd= F
)ddF G
)ddG H
{ee 
_loggerff 
.ff 
LogErrorff 
(ff 
$strff ;
)ff; <
;ff< =
returngg 
nullgg 
;gg 
}hh 
varjj 
	stationIdjj 
=jj 
	idElementjj 
.jj 
GetInt32jj &
(jj& '
)jj' (
;jj( )
_loggerkk 
.kk 
LogDebugkk 
(kk 
$strkk :
,kk: ;
	stationIdkk< E
)kkE F
;kkF G
trymm 
{nn 
stringpp 
namepp 
=pp 
nullpp 
;pp 
ifqq 
(qq	 

stationElementqq
 
.qq 
TryGetPropertyqq '
(qq' (
$strqq( .
,qq. /
outqq0 3
varqq4 7
nameElementqq8 C
)qqC D
)qqD E
{rr 
namess 
=ss 
nameElementss 
.ss 
	ValueKindss $
==ss% '
JsonValueKindss( 5
.ss5 6
Numberss6 <
?tt 
nameElementtt 
.tt 
GetInt32tt "
(tt" #
)tt# $
.tt$ %
ToStringtt% -
(tt- .
)tt. /
:uu 
nameElementuu 
.uu 
	GetStringuu #
(uu# $
)uu$ %
;uu% &
}vv 
elseww 

{xx 
throwyy 
newyy  
KeyNotFoundExceptionyy &
(yy& '
$stryy' H
)yyH I
;yyI J
}zz 
string}} 
postCode}} 
=}} 
$str}} 
;}}  
if~~ 
(~~	 

stationElement~~
 
.~~ 
TryGetProperty~~ '
(~~' (
$str~~( 3
,~~3 4
out~~5 8
var~~9 <
postCodeElement~~= L
)~~L M
)~~M N
{ 
postCode
ÄÄ 
=
ÄÄ 
postCodeElement
ÄÄ "
.
ÄÄ" #
	ValueKind
ÄÄ# ,
==
ÄÄ- /
JsonValueKind
ÄÄ0 =
.
ÄÄ= >
Number
ÄÄ> D
?
ÅÅ 
postCodeElement
ÅÅ 
.
ÅÅ 
GetInt32
ÅÅ &
(
ÅÅ& '
)
ÅÅ' (
.
ÅÅ( )
ToString
ÅÅ) 1
(
ÅÅ1 2
$str
ÅÅ2 6
)
ÅÅ6 7
:
ÇÇ 
postCodeElement
ÇÇ 
.
ÇÇ 
	GetString
ÇÇ '
(
ÇÇ' (
)
ÇÇ( )
??
ÇÇ* ,
$str
ÇÇ- 4
;
ÇÇ4 5
}
ÉÉ 
string
ÜÜ 
address
ÜÜ 
=
ÜÜ 
null
ÜÜ 
;
ÜÜ 
if
áá 
(
áá	 

stationElement
áá
 
.
áá 
TryGetProperty
áá '
(
áá' (
$str
áá( 1
,
áá1 2
out
áá3 6
var
áá7 :
addressElement
áá; I
)
ááI J
)
ááJ K
{
àà 
address
ââ 
=
ââ 
addressElement
ââ  
.
ââ  !
	ValueKind
ââ! *
==
ââ+ -
JsonValueKind
ââ. ;
.
ââ; <
Number
ââ< B
?
ää 
addressElement
ää 
.
ää 
GetInt32
ää %
(
ää% &
)
ää& '
.
ää' (
ToString
ää( 0
(
ää0 1
)
ää1 2
:
ãã 
addressElement
ãã 
.
ãã 
	GetString
ãã &
(
ãã& '
)
ãã' (
;
ãã( )
}
åå 
else
çç 

{
éé 
throw
èè 
new
èè "
KeyNotFoundException
èè &
(
èè& '
$str
èè' K
)
èèK L
;
èèL M
}
êê 
string
ìì 
crossStreet
ìì 
=
ìì 
null
ìì 
;
ìì  
if
îî 
(
îî	 

stationElement
îî
 
.
îî 
TryGetProperty
îî '
(
îî' (
$str
îî( 6
,
îî6 7
out
îî8 ;
var
îî< ? 
crossStreetElement
îî@ R
)
îîR S
&&
îîT V 
crossStreetElement
ïï
 
.
ïï 
	ValueKind
ïï &
!=
ïï' )
JsonValueKind
ïï* 7
.
ïï7 8
Null
ïï8 <
)
ïï< =
{
ññ 
crossStreet
óó 
=
óó  
crossStreetElement
óó (
.
óó( )
	ValueKind
óó) 2
==
óó3 5
JsonValueKind
óó6 C
.
óóC D
Number
óóD J
?
òò  
crossStreetElement
òò  
.
òò  !
GetInt32
òò! )
(
òò) *
)
òò* +
.
òò+ ,
ToString
òò, 4
(
òò4 5
)
òò5 6
:
ôô  
crossStreetElement
ôô  
.
ôô  !
	GetString
ôô! *
(
ôô* +
)
ôô+ ,
;
ôô, -
}
öö 
return
úú 
new
úú !
BicingStationEntity
úú $
{
ùù 
BicingId
ûû 
=
ûû 
	stationId
ûû 
,
ûû 

BicingName
üü 
=
üü 
name
üü 
,
üü 
Latitude
†† 
=
†† 
stationElement
†† !
.
††! "
TryGetProperty
††" 0
(
††0 1
$str
††1 6
,
††6 7
out
††8 ;
var
††< ?

latElement
††@ J
)
††J K
&&
††L N

latElement
††O Y
.
††Y Z
	ValueKind
††Z c
!=
††d f
JsonValueKind
††g t
.
††t u
Null
††u y
?
°° 

latElement
°° 
.
°° 
	GetDouble
°° $
(
°°$ %
)
°°% &
:
¢¢ 
$num
¢¢ 
,
¢¢ 
	Longitude
££ 
=
££ 
stationElement
££ "
.
££" #
TryGetProperty
££# 1
(
££1 2
$str
££2 7
,
££7 8
out
££9 <
var
££= @

lonElement
££A K
)
££K L
&&
££M O

lonElement
££P Z
.
££Z [
	ValueKind
££[ d
!=
££e g
JsonValueKind
££h u
.
££u v
Null
££v z
?
§§ 

lonElement
§§ 
.
§§ 
	GetDouble
§§ $
(
§§$ %
)
§§% &
:
•• 
$num
•• 
,
•• 
Altitude
¶¶ 
=
¶¶ 
stationElement
¶¶ !
.
¶¶! "
TryGetProperty
¶¶" 0
(
¶¶0 1
$str
¶¶1 ;
,
¶¶; <
out
¶¶= @
var
¶¶A D

altElement
¶¶E O
)
¶¶O P
&&
¶¶Q S

altElement
¶¶T ^
.
¶¶^ _
	ValueKind
¶¶_ h
!=
¶¶i k
JsonValueKind
¶¶l y
.
¶¶y z
Null
¶¶z ~
?
ßß 

altElement
ßß 
.
ßß 
	GetDouble
ßß $
(
ßß$ %
)
ßß% &
:
®® 
$num
®® 
,
®® 
Address
©© 
=
©© 
address
©© 
,
©© 
CrossStreet
™™ 
=
™™ 
crossStreet
™™ !
,
™™! "
PostCode
´´ 
=
´´ 
postCode
´´ 
,
´´ 
Capacity
¨¨ 
=
¨¨ 
stationElement
¨¨ !
.
¨¨! "
TryGetProperty
¨¨" 0
(
¨¨0 1
$str
¨¨1 ;
,
¨¨; <
out
¨¨= @
var
¨¨A D
capacityElement
¨¨E T
)
¨¨T U
&&
¨¨V X
capacityElement
¨¨Y h
.
¨¨h i
	ValueKind
¨¨i r
!=
¨¨s u
JsonValueKind¨¨v É
.¨¨É Ñ
Null¨¨Ñ à
?
≠≠ 
capacityElement
≠≠ 
.
≠≠  
GetInt32
≠≠  (
(
≠≠( )
)
≠≠) *
:
ÆÆ 
$num
ÆÆ 
,
ÆÆ 
IsChargingStation
ØØ 
=
ØØ 
stationElement
ØØ *
.
ØØ* +
TryGetProperty
ØØ+ 9
(
ØØ9 :
$str
ØØ: O
,
ØØO P
out
ØØQ T
var
ØØU X
chargingElement
ØØY h
)
ØØh i
&&
ØØj l
chargingElement
ØØm |
.
ØØ| }
	ValueKindØØ} Ü
!=ØØá â
JsonValueKindØØä ó
.ØØó ò
NullØØò ú
?
∞∞ 
chargingElement
∞∞ 
.
∞∞  

GetBoolean
∞∞  *
(
∞∞* +
)
∞∞+ ,
:
±± 
false
±± 
}
≤≤ 
;
≤≤ 
}
≥≥ 
catch
¥¥ 	
(
¥¥
 
	Exception
¥¥ 
ex
¥¥ 
)
¥¥ 
{
µµ 
_logger
∂∂ 
.
∂∂ 
LogError
∂∂ 
(
∂∂ 
ex
∂∂ 
,
∂∂ 
$str
∂∂ T
,
∂∂T U
	stationId
∂∂V _
)
∂∂_ `
;
∂∂` a
return
∑∑ 
null
∑∑ 
;
∑∑ 
}
∏∏ 
}
ππ 
}∫∫ ì 
c/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Services/BicingStationBackgroundService.cs
	namespace		 	
Services		
 
;		 
public 
class *
BicingStationBackgroundService +
:, -
BackgroundService. ?
{ 
private 	
readonly
 
IServiceProvider #
_serviceProvider$ 4
;4 5
private 	
readonly
 
ILogger 
< *
BicingStationBackgroundService 9
>9 :
_logger; B
;B C
private 	
readonly
 
TimeSpan 
	_interval %
=& '
TimeSpan( 0
.0 1
FromMinutes1 <
(< =
$num= ?
)? @
;@ A
public *
BicingStationBackgroundService	 '
(' (
IServiceProvider 
serviceProvider &
,& '
ILogger 
< *
BicingStationBackgroundService ,
>, -
logger. 4
)4 5
{ 
_serviceProvider 
= 
serviceProvider &
;& '
_logger 
= 
logger 
; 
} 
	protected 
override 
async 
Task 
ExecuteAsync  ,
(, -
CancellationToken- >
stoppingToken? L
)L M
{ 
while 	
(
 
! 
stoppingToken 
. #
IsCancellationRequested 1
)1 2
{ 
try 	
{ 
const 
int 
maxWaitTimeSeconds $
=% &
$num' )
;) *
const   
int   
checkIntervalMs   !
=  " #
$num  $ (
;  ( )
int!! 
elapsedSeconds!! 
=!! 
$num!! 
;!! 
while$$ 
($$ 
elapsedSeconds$$ 
<$$ 
maxWaitTimeSeconds$$  2
)$$2 3
{%% 	
await&&
 
Task&& 
.&& 
Delay&& 
(&& 
checkIntervalMs&& *
,&&* +
stoppingToken&&, 9
)&&9 :
.&&: ;
ConfigureAwait&&; I
(&&I J
false&&J O
)&&O P
;&&P Q
elapsedSeconds''
 
++'' 
;'' 
}(( 	
_logger** 
.** 
LogInformation** 
(** 
$str** L
)**L M
;**M N
using-- 
(-- 
var-- 
scope-- 
=-- 
_serviceProvider-- +
.--+ ,
CreateScope--, 7
(--7 8
)--8 9
)--9 :
{.. 	
var//
 
bicingService// 
=// 
scope// #
.//# $
ServiceProvider//$ 3
.//3 4
GetRequiredService//4 F
<//F G!
IBicingStationService//G \
>//\ ]
(//] ^
)//^ _
;//_ `
await00
 
bicingService00 
.00 ,
 FetchAndStoreBicingStationsAsync00 >
(00> ?
)00? @
.00@ A
ConfigureAwait00A O
(00O P
false00P U
)00U V
;00V W
}11 	
_logger33 
.33 
LogInformation33 
(33 
$str33 M
)33M N
;33N O
}44 
catch55 
(55 
	Exception55 
ex55 
)55 
{66 
_logger77 
.77 
LogError77 
(77 
ex77 
,77 
$str77 N
)77N O
;77O P
}88 
await:: 
Task:: 
.:: 
Delay:: 
(:: 
	_interval::  
,::  !
stoppingToken::" /
)::/ 0
.::0 1
ConfigureAwait::1 ?
(::? @
false::@ E
)::E F
;::F G
};; 
}<< 
}== éZ
W/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Repositories/UserRepository.cs
	namespace 	
Repositories
 
; 
public 
class 
UserRepository 
: 
IUserRepository -
{ 
private 	
readonly
 
ApiDbContext 

_Dbcontext  *
;* +
private 	
readonly
 
IMapper 
_mapper "
;" #
public 
UserRepository	 
( 
ApiDbContext $
context% ,
,, -
IMapper. 5
mapper6 <
)< =
{ 

_Dbcontext 
= 
context 
; 
_mapper 
= 
mapper 
; 
} 
public 
List	 
< 
UserDto 
> 
GetAllUsers "
(" #
)# $
{ 
var 
entities 
= 

_Dbcontext 
. 
Users #
. 	
Select	 
( 
u 
=> 
new 
UserDto  
{ 	
UserId
 
= 
u 
. 
UserId 
. 
ToString $
($ %
)% &
,& '
Username
 
= 
u 
. 
Username 
,  
Email  
 
=   
u   
.   
Email   
,   
PasswordHash!!
 
=!! 
u!! 
.!! 
PasswordHash!! '
,!!' (
}"" 	
)""	 

.## 	
ToList##	 
(## 
)## 
;## 
return$$ 

_mapper$$ 
.$$ 
Map$$ 
<$$ 
List$$ 
<$$ 
UserDto$$ #
>$$# $
>$$$ %
($$% &
entities$$& .
)$$. /
;$$/ 0
}%% 
public'' 
bool''	 

CreateUser'' 
('' 

UserCreate'' #
userDto''$ +
)''+ ,
{(( 
try)) 
{** 
var++ 	
passwordHasher++
 
=++ 
new++  
PasswordHasherHelper++ 3
(++3 4
)++4 5
;++5 6
var,, 	
password,,
 
=,, 
passwordHasher,, #
.,,# $
HashPassword,,$ 0
(,,0 1
userDto,,1 8
.,,8 9
PasswordHash,,9 E
),,E F
;,,F G
var.. 	
user..
 
=.. 
new.. 

UserEntity.. 
{// 
UserId00 
=00 
Guid00 
.00 
NewGuid00 
(00 
)00 
,00  
Username11 
=11 
userDto11 
.11 
Username11 #
,11# $
Email22 
=22 
userDto22 
.22 
Email22 
,22 
PasswordHash33 
=33 
password33 
,33  
}44 
;44 

_Dbcontext66 
.66 
Users66 
.66 
Add66 
(66 
user66 
)66  
;66  !
return77 

_Dbcontext77 
.77 
SaveChanges77 #
(77# $
)77$ %
>77& '
$num77( )
;77) *
}88 
catch99 	
(99
 
	Exception99 
)99 
{:: 
return;; 
false;; 
;;; 
}<< 
}== 
public>> 
async>>	 
Task>> 
<>> 
UserDto>> 
>>> 
GetUserByUsername>> .
(>>. /
string>>/ 5
username>>6 >
)>>> ?
{?? 
var@@ 
user@@ 
=@@ 
await@@ 

_Dbcontext@@ 
.@@  
Users@@  %
.AA 	
FirstOrDefaultAsyncAA	 
(AA 
uAA 
=>AA !
uAA" #
.AA# $
UsernameAA$ ,
==AA- /
usernameAA0 8
)AA8 9
.AA9 :
ConfigureAwaitAA: H
(AAH I
falseAAI N
)AAN O
;AAO P
returnCC 

_mapperCC 
.CC 
MapCC 
<CC 
UserDtoCC 
>CC 
(CC  
userCC  $
)CC$ %
;CC% &
}DD 
publicFF 
asyncFF	 
TaskFF 
<FF 
boolFF 
>FF 

DeleteUserFF $
(FF$ %
stringFF% +
userIdFF, 2
)FF2 3
{GG 
ifHH 
(HH 
!HH 	
GuidHH	 
.HH 
TryParseHH 
(HH 
userIdHH 
,HH 
outHH "
GuidHH# '
parsedUserIdHH( 4
)HH4 5
)HH5 6
returnII 
falseII 
;II 
varKK 
userKK 
=KK 
awaitKK 

_DbcontextKK 
.KK  
UsersKK  %
.LL 	
FirstOrDefaultAsyncLL	 
(LL 
uLL 
=>LL !
uLL" #
.LL# $
UserIdLL$ *
==LL+ -
parsedUserIdLL. :
)LL: ;
.LL; <
ConfigureAwaitLL< J
(LLJ K
falseLLK P
)LLP Q
;LLQ R
ifMM 
(MM 
userMM 
==MM 
nullMM 
)MM 
{NN 
returnOO 
falseOO 
;OO 
}PP 
awaitRR 	

_DbcontextRR
 
.RR 
SavedUbicationsRR $
.SS 	
WhereSS	 
(SS 
uSS 
=>SS 
uSS 
.SS 
	UserEmailSS 
==SS  "
userSS# '
.SS' (
EmailSS( -
)SS- .
.TT 	
ExecuteDeleteAsyncTT	 
(TT 
)TT 
.TT 
ConfigureAwaitTT ,
(TT, -
falseTT- 2
)TT2 3
;TT3 4
intVV 
deletedRowsVV 
=VV 
awaitVV 

_DbcontextVV &
.VV& '
UsersVV' ,
.WW 	
WhereWW	 
(WW 
uWW 
=>WW 
uWW 
.WW 
UserIdWW 
==WW 
parsedUserIdWW  ,
)WW, -
.XX 	
ExecuteDeleteAsyncXX	 
(XX 
)XX 
.XX 
ConfigureAwaitXX ,
(XX, -
falseXX- 2
)XX2 3
;XX3 4
returnZZ 

deletedRowsZZ 
>ZZ 
$numZZ 
;ZZ 
}[[ 
public]] 
async]]	 
Task]] 
<]] 
bool]] 
>]] 

ModifyUser]] $
(]]$ %
UserDto]]% ,

userModify]]- 7
)]]7 8
{^^ 
var__ 
user__ 
=__ 
await__ 

_Dbcontext__ 
.__  
Users__  %
.__% &
FirstOrDefaultAsync__& 9
(__9 :
u__: ;
=>__< >
u__? @
.__@ A
UserId__A G
==__H J
Guid__K O
.__O P
Parse__P U
(__U V

userModify__V `
.__` a
UserId__a g
)__g h
)__h i
.__i j
ConfigureAwait__j x
(__x y
false__y ~
)__~ 
;	__ Ä
if`` 
(`` 
user`` 
==`` 
null`` 
)`` 
{aa 
returnbb 
falsebb 
;bb 
}cc 
ifdd 
(dd 

userModifydd 
.dd 
PasswordHashdd 
!=dd  "
nulldd# '
&&dd( *
!dd+ ,
stringdd, 2
.dd2 3
IsNullOrEmptydd3 @
(dd@ A

userModifyddA K
.ddK L
PasswordHashddL X
)ddX Y
)ddY Z
{ee 
varff 	
passwordHasherff
 
=ff 
newff  
PasswordHasherHelperff 3
(ff3 4
)ff4 5
;ff5 6
vargg 	
passwordgg
 
=gg 
passwordHashergg #
.gg# $
HashPasswordgg$ 0
(gg0 1

userModifygg1 ;
.gg; <
PasswordHashgg< H
)ggH I
;ggI J
userhh 

.hh
 
PasswordHashhh 
=hh 
passwordhh "
;hh" #
}ii 
userkk 
.kk 	
Usernamekk	 
=kk 

userModifykk 
.kk 
Usernamekk '
;kk' (

_Dbcontextmm 
.mm 
Usersmm 
.mm 
Updatemm 
(mm 
usermm  
)mm  !
;mm! "
returnnn 

awaitnn 

_Dbcontextnn 
.nn 
SaveChangesAsyncnn ,
(nn, -
)nn- .
.nn. /
ConfigureAwaitnn/ =
(nn= >
falsenn> C
)nnC D
>nnE F
$numnnG H
;nnH I
}oo 
publicqq 
asyncqq	 
Taskqq 
<qq 
UserDtoqq 
?qq 
>qq 
GetUserByEmailAsyncqq 1
(qq1 2
stringqq2 8
emailqq9 >
)qq> ?
{rr 
varss 
userss 
=ss 
awaitss 

_Dbcontextss 
.ss  
Usersss  %
.ss% &
FirstOrDefaultAsyncss& 9
(ss9 :
uss: ;
=>ss< >
uss? @
.ss@ A
EmailssA F
==ssG I
emailssJ O
)ssO P
.ssP Q
ConfigureAwaitssQ _
(ss_ `
falsess` e
)sse f
;ssf g
returntt 

_mappertt 
.tt 
Maptt 
<tt 
UserDtott 
>tt 
(tt  
usertt  $
)tt$ %
;tt% &
}uu 
publicww 
asyncww	 
Taskww 
<ww 
boolww 
>ww !
CreateGoogleUserAsyncww /
(ww/ 0
stringww0 6
nameww7 ;
,ww; <
stringww= C
emailwwD I
)wwI J
{xx 
tryyy 
{zz 
var{{ 	
user{{
 
={{ 
new{{ 

UserEntity{{ 
{|| 
UserId}} 
=}} 
Guid}} 
.}} 
NewGuid}} 
(}} 
)}} 
,}}  
Username~~ 
=~~ 
name~~ 
,~~ 
Email 
= 
email 
, 
PasswordHash
ÄÄ 
=
ÄÄ 
$str
ÄÄ 
,
ÄÄ 
}
ÅÅ 
;
ÅÅ 

_Dbcontext
ÉÉ 
.
ÉÉ 
Users
ÉÉ 
.
ÉÉ 
Add
ÉÉ 
(
ÉÉ 
user
ÉÉ 
)
ÉÉ  
;
ÉÉ  !
return
ÑÑ 
await
ÑÑ 

_Dbcontext
ÑÑ 
.
ÑÑ 
SaveChangesAsync
ÑÑ .
(
ÑÑ. /
)
ÑÑ/ 0
.
ÑÑ0 1
ConfigureAwait
ÑÑ1 ?
(
ÑÑ? @
false
ÑÑ@ E
)
ÑÑE F
>
ÑÑG H
$num
ÑÑI J
;
ÑÑJ K
}
ÖÖ 
catch
ÜÜ 	
{
áá 
return
àà 
false
àà 
;
àà 
}
ââ 
}
ää 
}ãã ∑6
\/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Repositories/UbicationRepository.cs
	namespace 	
Repositories
 
; 
public 
class 
UbicationRepository  
:! " 
IUbicationRepository# 7
{ 
private 	
readonly
 
ApiDbContext 
_context  (
;( )
private 	
readonly
 
IMapper 
_mapper "
;" #
public 
UbicationRepository	 
( 
ApiDbContext )
context* 1
,1 2
IMapper3 :
mapper; A
)A B
{ 
_mapper 
= 
mapper 
; 
_context 
= 
context 
; 
} 
public 
async	 
Task 
< 
List 
< 
SavedUbicationDto *
>* +
>+ ,&
GetUbicationsByUserIdAsync- G
(G H
stringH N
	userEmailO X
)X Y
{ 
var 
entities 
= 
await 
_context !
.! "
SavedUbications" 1
. 	
Where	 
( 
u 
=> 
u 
. 
	UserEmail 
==  "
	userEmail# ,
), -
. 	
ToListAsync	 
( 
) 
. 
ConfigureAwait %
(% &
false& +
)+ ,
;, -
return 

_mapper 
. 
Map 
< 
List 
< 
SavedUbicationDto -
>- .
>. /
(/ 0
entities0 8
)8 9
;9 :
} 
public 
async	 
Task 
< 
bool 
> 
SaveUbicationAsync ,
(, -
SavedUbicationDto- >
savedUbication? M
)M N
{   
var!!  
savedUbicationEntity!! 
=!! 
new!! " 
SavedUbicationEntity!!# 7
{"" 
	UserEmail## 
=## 
savedUbication##  
.##  !
	UserEmail##! *
,##* +
UbicationId$$ 
=$$ 
savedUbication$$ "
.$$" #
UbicationId$$# .
,$$. /
Latitude%% 
=%% 
savedUbication%% 
.%%  
Latitude%%  (
,%%( )
	Longitude&& 
=&& 
savedUbication&&  
.&&  !
	Longitude&&! *
,&&* +
StationType'' 
='' 
savedUbication'' "
.''" #
StationType''# .
,''. /
}(( 
;(( 
await)) 	
_context))
 
.)) 
SavedUbications)) "
.))" #
AddAsync))# +
())+ , 
savedUbicationEntity)), @
)))@ A
.))A B
ConfigureAwait))B P
())P Q
false))Q V
)))V W
;))W X
return** 

await** 
_context** 
.** 
SaveChangesAsync** *
(*** +
)**+ ,
.**, -
ConfigureAwait**- ;
(**; <
false**< A
)**A B
>**C D
$num**E F
;**F G
}++ 
public,, 
async,,	 
Task,, 
<,, 
bool,, 
>,, 
DeleteUbication,, )
(,,) *
UbicationInfoDto,,* :
ubicationDelete,,; J
),,J K
{-- 
var.. 
savedUbication.. 
=.. 
await.. 
_context.. '
...' (
SavedUbications..( 7
.// 	
FirstOrDefaultAsync//	 
(// 
u// 
=>// !
u//" #
.//# $
	UserEmail//$ -
==//. 0
ubicationDelete//1 @
.//@ A
Username//A I
&&//J L
u//M N
.//N O
UbicationId//O Z
==//[ ]
ubicationDelete//^ m
.//m n
UbicationId//n y
&&//z |
u//} ~
.//~ 
StationType	// ä
==
//ã ç
ubicationDelete
//é ù
.
//ù û
StationType
//û ©
)
//© ™
.
//™ ´
ConfigureAwait
//´ π
(
//π ∫
false
//∫ ø
)
//ø ¿
;
//¿ ¡
if00 
(00 
savedUbication00 
==00 
null00 
)00 
{11 
return22 
false22 
;22 
}33 
_context44 
.44 
SavedUbications44 
.44 
Remove44 #
(44# $
savedUbication44$ 2
)442 3
;443 4
return55 

await55 
_context55 
.55 
SaveChangesAsync55 *
(55* +
)55+ ,
.55, -
ConfigureAwait55- ;
(55; <
false55< A
)55A B
>55C D
$num55E F
;55F G
}66 
public77 
async77	 
Task77 
<77 
bool77 
>77 
UpdateUbication77 )
(77) *
UbicationInfoDto77* :
savedUbication77; I
)77I J
{88 
var99  
savedUbicationEntity99 
=99 
await99 $
_context99% -
.99- .
SavedUbications99. =
.:: 	
FirstOrDefaultAsync::	 
(:: 
u:: 
=>:: !
u::" #
.::# $
	UserEmail::$ -
==::. 0
savedUbication::1 ?
.::? @
Username::@ H
&&::I K
u::L M
.::M N
UbicationId::N Y
==::Z \
savedUbication::] k
.::k l
UbicationId::l w
)::w x
.::x y
ConfigureAwait	::y á
(
::á à
false
::à ç
)
::ç é
;
::é è
if;; 
(;;  
savedUbicationEntity;; 
==;; 
null;;  $
);;$ %
{<< 
return== 
false== 
;== 
}>>  
savedUbicationEntity?? 
.?? 

Valoration?? #
=??$ %
savedUbication??& 4
.??4 5

Valoration??5 ?
;??? @ 
savedUbicationEntity@@ 
.@@ 
Comment@@  
=@@! "
savedUbication@@# 1
.@@1 2
Comment@@2 9
;@@9 :
returnAA 

awaitAA 
_contextAA 
.AA 
SaveChangesAsyncAA *
(AA* +
)AA+ ,
.AA, -
ConfigureAwaitAA- ;
(AA; <
falseAA< A
)AAA B
>AAC D
$numAAE F
;AAF G
}BB 
}CC Õ.
^/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Repositories/StateBicingRepository.cs
	namespace

 	
Repositories


 
;

 
public 
class !
StateBicingRepository "
:# $"
IStateBicingRepository% ;
{ 
private 	
readonly
 
ApiDbContext 

_dbContext  *
;* +
private 	
readonly
 
IMapper 
_mapper "
;" #
public !
StateBicingRepository	 
( 
ApiDbContext +
	dbContext, 5
,5 6
IMapper7 >
mapper? E
)E F
{ 

_dbContext 
= 
	dbContext 
; 
_mapper 
= 
mapper 
; 
} 
public 
async	 
Task 
BulkInsertAsync #
(# $
List 

<
 
StateBicingEntity 
> 
statebicingstations 1
)1 2
{ 
using 	
(
 
var 
transaction 
= 
await "

_dbContext# -
.- .
Database. 6
.6 7!
BeginTransactionAsync7 L
(L M
)M N
.N O
ConfigureAwaitO ]
(] ^
false^ c
)c d
)d e
{ 
try 	
{ 
await 

_dbContext 
. 
Database !
.! "
ExecuteSqlRawAsync" 4
(4 5
$str5 Z
)Z [
.[ \
ConfigureAwait\ j
(j k
falsek p
)p q
;q r
await   

_dbContext   
.   
StateBicing   $
.  $ %
AddRangeAsync  % 2
(  2 3
statebicingstations  3 F
)  F G
.  G H
ConfigureAwait  H V
(  V W
false  W \
)  \ ]
;  ] ^
await!! 

_dbContext!! 
.!! 
SaveChangesAsync!! )
(!!) *
)!!* +
.!!+ ,
ConfigureAwait!!, :
(!!: ;
false!!; @
)!!@ A
;!!A B
await## 
transaction## 
.## 
CommitAsync## %
(##% &
)##& '
.##' (
ConfigureAwait##( 6
(##6 7
false##7 <
)##< =
;##= >
}$$ 
catch%% 
(%% 
	Exception%% 
)%% 
{&& 
await'' 
transaction'' 
.'' 
RollbackAsync'' '
(''' (
)''( )
.'') *
ConfigureAwait''* 8
(''8 9
false''9 >
)''> ?
;''? @
throw(( 
;(( 
})) 
}** 
}++ 
public-- 
async--	 
Task-- 
<-- 
List-- 
<-- 
StateBicingDto-- '
>--' (
>--( )%
GetAllStateBicingStations--* C
(--C D
)--D E
{.. 
var// 
entities// 
=// 
await// 

_dbContext// #
.//# $
StateBicing//$ /
.00 	
Select00	 
(00 
s00 
=>00 
new00 
StateBicingEntity00 *
{11 	
BicingId22
 
=22 
s22 
.22 
BicingId22 
,22  
NumBikesAvailable33
 
=33 
s33 
.33  
NumBikesAvailable33  1
,331 2'
NumBikesAvailableMechanical44
 %
=44& '
s44( )
.44) *'
NumBikesAvailableMechanical44* E
,44E F"
NumBikesAvailableEbike55
  
=55! "
s55# $
.55$ %"
NumBikesAvailableEbike55% ;
,55; <
NumDocksAvailable66
 
=66 
s66 
.66  
NumDocksAvailable66  1
,661 2
LastReported77
 
=77 
s77 
.77 
LastReported77 '
,77' (
Status88
 
=88 
s88 
.88 
Status88 
,88 %
BicingStationIdNavigation99
 #
=99$ %
s99& '
.99' (%
BicingStationIdNavigation99( A
}:: 	
)::	 

.;; 	
ToListAsync;;	 
(;; 
);; 
.;; 
ConfigureAwait;; %
(;;% &
false;;& +
);;+ ,
;;;, -
return== 

_mapper== 
.== 
Map== 
<== 
List== 
<== 
StateBicingDto== *
>==* +
>==+ ,
(==, -
entities==- 5
)==5 6
;==6 7
}>> 
public@@ 
async@@	 
Task@@ 
<@@ 
StateBicingDto@@ "
>@@" #
GetStateBicingById@@$ 6
(@@6 7
int@@7 :
id@@; =
)@@= >
{AA 
varBB 
entityBB 
=BB 
awaitBB 

_dbContextBB !
.BB! "
StateBicingBB" -
.CC 	
FirstOrDefaultAsyncCC	 
(CC 
sCC 
=>CC !
sCC" #
.CC# $
BicingIdCC$ ,
==CC- /
idCC0 2
)CC2 3
.CC3 4
ConfigureAwaitCC4 B
(CCB C
falseCCC H
)CCH I
;CCI J
ifEE 
(EE 
entityEE 
==EE 
nullEE 
)EE 
{FF 
returnGG 
nullGG 
;GG 
}HH 
returnJJ 

_mapperJJ 
.JJ 
MapJJ 
<JJ 
StateBicingDtoJJ %
>JJ% &
(JJ& '
entityJJ' -
)JJ- .
;JJ. /
}KK 
}MM Û
X/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Repositories/RouteRepository.cs
public		 
class		 
RouteRepository		 
:		 
IRouteRepository		 /
{

 
private 	
readonly
 
ApiDbContext 

_dbContext  *
;* +
public 
RouteRepository	 
( 
ApiDbContext %
	dbContext& /
)/ 0
{ 

_dbContext 
= 
	dbContext 
; 
} 
public 
async	 
Task 
GuardarRutaAsync $
($ %
RouteEntity% 0
ruta1 5
)5 6
{ 

_dbContext 
. 
Routes 
. 
Add 
( 
ruta 
) 
;  
await 	

_dbContext
 
. 
SaveChangesAsync %
(% &
)& '
.' (
ConfigureAwait( 6
(6 7
false7 <
)< =
;= >
} 
} Ì
b/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Repositories/Interface/IUserRepository.cs
	namespace 	
Repositories
 
. 
	Interface  
;  !
public 
	interface 
IUserRepository  
{ 
public		 
List			 
<		 
UserDto		 
>		 
GetAllUsers		 "
(		" #
)		# $
;		$ %
public

 
bool

	 

CreateUser

 
(

 

UserCreate

 #
user

$ (
)

( )
;

) *
public 
Task	 
< 
UserDto 
> 
GetUserByUsername (
(( )
string) /
username0 8
)8 9
;9 :
public 
Task	 
< 
bool 
> 

DeleteUser 
( 
string %
userId& ,
), -
;- .
public 
Task	 
< 
bool 
> 

ModifyUser 
( 
UserDto &

userModify' 1
)1 2
;2 3
Task 
< 
UserDto 
? 
> 
GetUserByEmailAsync $
($ %
string% +
email, 1
)1 2
;2 3
Task 
< 
bool 
> !
CreateGoogleUserAsync "
(" #
string# )
name* .
,. /
string0 6
email7 <
)< =
;= >
} à	
g/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Repositories/Interface/IUbicationRepository.cs
	namespace 	
Repositories
 
. 
	Interface  
;  !
public 
	interface  
IUbicationRepository %
{ 
Task		 
<		 
List		 
<		 
SavedUbicationDto		 
>		 
>		 &
GetUbicationsByUserIdAsync		  :
(		: ;
string		; A
	userEmail		B K
)		K L
;		L M
Task

 
<

 
bool

 
>

 
SaveUbicationAsync

 
(

  
SavedUbicationDto

  1
savedUbication

2 @
)

@ A
;

A B
Task 
< 
bool 
> 
DeleteUbication 
( 
UbicationInfoDto -
ubicationDelete. =
)= >
;> ?
Task 
< 
bool 
> 
UpdateUbication 
( 
UbicationInfoDto -
savedUbication. <
)< =
;= >
} ã
i/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Repositories/Interface/IStateBicingRepository.cs
	namespace 	
Repositories
 
. 
	Interface  
;  !
public 
	interface "
IStateBicingRepository '
{ 
Task		 
<		 
List		 
<		 
StateBicingDto		 
>		 
>		 %
GetAllStateBicingStations		 6
(		6 7
)		7 8
;		8 9
Task 
< 
StateBicingDto 
> 
GetStateBicingById )
() *
int* -
	stationId. 7
)7 8
;8 9
Task 
BulkInsertAsync 
( 
List 

<
 
StateBicingEntity 
> 
statebicing )
) 
; 
} Œ
c/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Repositories/Interface/IRouteRepository.cs
public 
	interface 
IRouteRepository !
{ 
Task 
GuardarRutaAsync 
( 
RouteEntity #
ruta$ (
)( )
;) *
} ‚	
n/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Repositories/Interface/IChargingStationsRepository.cs
	namespace 	
Repositories
 
. 
	Interface  
;  !
public 
	interface '
IChargingStationsRepository ,
{ 
Task		 
<		 
List		 
<		 
ChargingStationDto		 
>		 
>		  "
GetAllChargingStations		! 7
(		7 8
)		8 9
;		9 :
Task

 
BulkInsertAsync

 
(

 
List 

<
 
LocationEntity 
> 
	locations $
,$ %
List 

<
 

HostEntity 
> 
hosts 
, 
List 

<
 
StationEntity 
> 
stations "
," #
List 

<
 

PortEntity 
> 
ports 
) 
; 
Task 
< 
ChargingStationDto 
> %
GetChargingStationDetails 4
(4 5
int5 8
ubicationId9 D
)D E
;E F
} Õt
c/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Repositories/ChargingStationsRepository.cs
	namespace

 	
Repositories


 
;

 
public 
class &
ChargingStationsRepository '
:( )'
IChargingStationsRepository* E
{ 
private 	
readonly
 
ApiDbContext 

_dbContext  *
;* +
private 	
readonly
 
IMapper 
_mapper "
;" #
public &
ChargingStationsRepository	 #
(# $
ApiDbContext$ 0
	dbContext1 :
,: ;
IMapper< C
mapperD J
)J K
{ 

_dbContext 
= 
	dbContext 
; 
_mapper 
= 
mapper 
; 
} 
public 
async	 
Task 
BulkInsertAsync #
(# $
List 

<
 
LocationEntity 
> 
	locations $
,$ %
List 

<
 

HostEntity 
> 
hosts 
, 
List 

<
 
StationEntity 
> 
stations "
," #
List 

<
 

PortEntity 
> 
ports 
) 
{ 
using 	
(
 
var 
transaction 
= 
await "

_dbContext# -
.- .
Database. 6
.6 7!
BeginTransactionAsync7 L
(L M
)M N
.N O
ConfigureAwaitO ]
(] ^
false^ c
)c d
)d e
{ 
try 	
{   
await!! 

_dbContext!! 
.!! 
Database!! !
.!!! "
ExecuteSqlRawAsync!!" 4
(!!4 5
$str!!5 V
)!!V W
.!!W X
ConfigureAwait!!X f
(!!f g
false!!g l
)!!l m
;!!m n
await"" 

_dbContext"" 
."" 
Database"" !
.""! "
ExecuteSqlRawAsync""" 4
(""4 5
$str""5 R
)""R S
.""S T
ConfigureAwait""T b
(""b c
false""c h
)""h i
;""i j
await## 

_dbContext## 
.## 
Database## !
.##! "
ExecuteSqlRawAsync##" 4
(##4 5
$str##5 U
)##U V
.##V W
ConfigureAwait##W e
(##e f
false##f k
)##k l
;##l m
await$$ 

_dbContext$$ 
.$$ 
Database$$ !
.$$! "
ExecuteSqlRawAsync$$" 4
($$4 5
$str$$5 R
)$$R S
.$$S T
ConfigureAwait$$T b
($$b c
false$$c h
)$$h i
;$$i j
await&& 

_dbContext&& 
.&& 
	Locations&& "
.&&" #
AddRangeAsync&&# 0
(&&0 1
	locations&&1 :
)&&: ;
.&&; <
ConfigureAwait&&< J
(&&J K
false&&K P
)&&P Q
;&&Q R
await'' 

_dbContext'' 
.'' 
SaveChangesAsync'' )
('') *
)''* +
.''+ ,
ConfigureAwait'', :
('': ;
false''; @
)''@ A
;''A B
await)) 

_dbContext)) 
.)) 
Hosts)) 
.)) 
AddRangeAsync)) ,
()), -
hosts))- 2
)))2 3
.))3 4
ConfigureAwait))4 B
())B C
false))C H
)))H I
;))I J
await** 

_dbContext** 
.** 
SaveChangesAsync** )
(**) *
)*** +
.**+ ,
ConfigureAwait**, :
(**: ;
false**; @
)**@ A
;**A B
await,, 

_dbContext,, 
.,, 
Stations,, !
.,,! "
AddRangeAsync,," /
(,,/ 0
stations,,0 8
),,8 9
.,,9 :
ConfigureAwait,,: H
(,,H I
false,,I N
),,N O
;,,O P
await-- 

_dbContext-- 
.-- 
SaveChangesAsync-- )
(--) *
)--* +
.--+ ,
ConfigureAwait--, :
(--: ;
false--; @
)--@ A
;--A B
await// 

_dbContext// 
.// 
Ports// 
.// 
AddRangeAsync// ,
(//, -
ports//- 2
)//2 3
.//3 4
ConfigureAwait//4 B
(//B C
false//C H
)//H I
;//I J
await00 

_dbContext00 
.00 
SaveChangesAsync00 )
(00) *
)00* +
.00+ ,
ConfigureAwait00, :
(00: ;
false00; @
)00@ A
;00A B
await22 
transaction22 
.22 
CommitAsync22 %
(22% &
)22& '
.22' (
ConfigureAwait22( 6
(226 7
false227 <
)22< =
;22= >
}33 
catch44 
(44 
	Exception44 
)44 
{55 
await66 
transaction66 
.66 
RollbackAsync66 '
(66' (
)66( )
.66) *
ConfigureAwait66* 8
(668 9
false669 >
)66> ?
;66? @
throw77 
;77 
}88 
}99 
}:: 
public<< 
async<<	 
Task<< 
<<< 
List<< 
<<< 
ChargingStationDto<< +
><<+ ,
><<, -"
GetAllChargingStations<<. D
(<<D E
)<<E F
{== 
var?? 
query?? 
=?? 
from@@ 
station@@ 
in@@ 

_dbContext@@ "
.@@" #
Stations@@# +
joinAA 
locationAA 
inAA 

_dbContextAA #
.AA# $
	LocationsAA$ -
onBB 
stationBB 
.BB 

LocationIdBB !
equalsBB" (
locationBB) 1
.BB1 2

LocationIdBB2 <
joinCC 
hostCC 
inCC 

_dbContextCC 
.CC  
HostsCC  %
onDD 
locationDD 
.DD 

LocationIdDD "
equalsDD# )
hostDD* .
.DD. /

LocationIdDD/ 9
joinEE 
portEE 
inEE 

_dbContextEE 
.EE  
PortsEE  %
onFF 
stationFF 
.FF 
	StationIdFF  
equalsFF! '
portFF( ,
.FF, -
	StationIdFF- 6
intoFF7 ;

portsGroupFF< F
selectGG 
newGG 
ChargingStationDtoGG %
{HH 	
	StationIdJJ
 
=JJ 
stationJJ 
.JJ 
	StationIdJJ '
,JJ' (
StationLabelKK
 
=KK 
stationKK  
.KK  !
StationLabelKK! -
,KK- .
StationLatitudeLL
 
=LL 
stationLL #
.LL# $
StationLatitudeLL$ 3
,LL3 4
StationLongitudeMM
 
=MM 
stationMM $
.MM$ %
StationLongitudeMM% 5
,MM5 6
AddressStringPP
 
=PP 
locationPP "
.PP" #
AddressStringPP# 0
,PP0 1
LocalityQQ
 
=QQ 
locationQQ 
.QQ 
LocalityQQ &
,QQ& '

PostalCodeRR
 
=RR 
locationRR 
.RR  

PostalCodeRR  *
,RR* +
LocationLatitudeSS
 
=SS 
locationSS %
.SS% &
LatitudeSS& .
,SS. /
LocationLongitudeTT
 
=TT 
locationTT &
.TT& '
	LongitudeTT' 0
,TT0 1
HostIdWW
 
=WW 
hostWW 
.WW 
HostIdWW 
.WW 
ToStringWW '
(WW' (
)WW( )
,WW) *
HostNameXX
 
=XX 
hostXX 
.XX 
HostNameXX "
,XX" #
	HostPhoneYY
 
=YY 
hostYY 
.YY 
OperatorPhoneYY (
,YY( )
HostWebsiteZZ
 
=ZZ 
hostZZ 
.ZZ 
OperatorWebsiteZZ ,
,ZZ, -
Ports]]
 
=]] 

portsGroup]] 
.]] 
Select]] #
(]]# $
p]]$ %
=>]]& (
new]]) ,
PortDto]]- 4
{^^
 
PortId__ 
=__ 
p__ 
.__ 
PortId__ 
,__ 
	StationId`` 
=`` 
p`` 
.`` 
	StationId`` #
,``# $
ConnectorTypeaa 
=aa 
paa 
.aa 
ConnectorTypeaa +
,aa+ ,
PowerKwbb 
=bb 
pbb 
.bb 
PowerKwbb 
,bb  

PortStatuscc 
=cc 
pcc 
.cc 
Statuscc !
,cc! "

Reservabledd 
=dd 
pdd 
.dd 

Reservabledd %
,dd% &
ChargingMechanismee 
=ee 
pee  !
.ee! "
ChargingMechanismee" 3
,ee3 4
LastUpdatedff 
=ff 
pff 
.ff 
LastUpdatedff '
}gg
 
)gg 
.gg 
ToListgg 
(gg 
)gg 
}hh 	
;hh	 

returnjj 

awaitjj 
queryjj 
.jj 
ToListAsyncjj "
(jj" #
)jj# $
.jj$ %
ConfigureAwaitjj% 3
(jj3 4
falsejj4 9
)jj9 :
;jj: ;
}kk 
publicll 
asyncll	 
Taskll 
<ll 
ChargingStationDtoll &
>ll& '%
GetChargingStationDetailsll( A
(llA B
intllB E
ubicationIdllF Q
)llQ R
{mm 
varnn 
querynn 
=nn 
fromoo 
stationoo 
inoo 

_dbContextoo "
.oo" #
Stationsoo# +
joinpp 
locationpp 
inpp 

_dbContextpp #
.pp# $
	Locationspp$ -
onqq 
stationqq 
.qq 

LocationIdqq !
equalsqq" (
locationqq) 1
.qq1 2

LocationIdqq2 <
joinrr 
hostrr 
inrr 

_dbContextrr 
.rr  
Hostsrr  %
onss 
locationss 
.ss 

LocationIdss "
equalsss# )
hostss* .
.ss. /

LocationIdss/ 9
jointt 
porttt 
intt 

_dbContexttt 
.tt  
Portstt  %
onuu 
stationuu 
.uu 
	StationIduu  
equalsuu! '
portuu( ,
.uu, -
	StationIduu- 6
intouu7 ;

portsGroupuu< F
wherevv 
stationvv 
.vv 
	StationIdvv 
==vv  "
ubicationIdvv# .
.vv. /
ToStringvv/ 7
(vv7 8
)vv8 9
selectww 
newww 
ChargingStationDtoww %
{xx 	
	StationIdzz
 
=zz 
stationzz 
.zz 
	StationIdzz '
,zz' (
StationLabel{{
 
={{ 
station{{  
.{{  !
StationLabel{{! -
,{{- .
StationLatitude||
 
=|| 
station|| #
.||# $
StationLatitude||$ 3
,||3 4
StationLongitude}}
 
=}} 
station}} $
.}}$ %
StationLongitude}}% 5
,}}5 6
AddressString
ÄÄ
 
=
ÄÄ 
location
ÄÄ "
.
ÄÄ" #
AddressString
ÄÄ# 0
,
ÄÄ0 1
Locality
ÅÅ
 
=
ÅÅ 
location
ÅÅ 
.
ÅÅ 
Locality
ÅÅ &
,
ÅÅ& '

PostalCode
ÇÇ
 
=
ÇÇ 
location
ÇÇ 
.
ÇÇ  

PostalCode
ÇÇ  *
,
ÇÇ* +
LocationLatitude
ÉÉ
 
=
ÉÉ 
location
ÉÉ %
.
ÉÉ% &
Latitude
ÉÉ& .
,
ÉÉ. /
LocationLongitude
ÑÑ
 
=
ÑÑ 
location
ÑÑ &
.
ÑÑ& '
	Longitude
ÑÑ' 0
,
ÑÑ0 1
HostId
áá
 
=
áá 
host
áá 
.
áá 
HostId
áá 
.
áá 
ToString
áá '
(
áá' (
)
áá( )
,
áá) *
HostName
àà
 
=
àà 
host
àà 
.
àà 
HostName
àà "
,
àà" #
	HostPhone
ââ
 
=
ââ 
host
ââ 
.
ââ 
OperatorPhone
ââ (
,
ââ( )
HostWebsite
ää
 
=
ää 
host
ää 
.
ää 
OperatorWebsite
ää ,
,
ää, -
Ports
çç
 
=
çç 

portsGroup
çç 
.
çç 
Select
çç #
(
çç# $
p
çç$ %
=>
çç& (
new
çç) ,
PortDto
çç- 4
{
éé
 
PortId
èè 
=
èè 
p
èè 
.
èè 
PortId
èè 
,
èè 
	StationId
êê 
=
êê 
p
êê 
.
êê 
	StationId
êê #
,
êê# $
ConnectorType
ëë 
=
ëë 
p
ëë 
.
ëë 
ConnectorType
ëë +
,
ëë+ ,
PowerKw
íí 
=
íí 
p
íí 
.
íí 
PowerKw
íí 
,
íí  

PortStatus
ìì 
=
ìì 
p
ìì 
.
ìì 
Status
ìì !
,
ìì! "

Reservable
îî 
=
îî 
p
îî 
.
îî 

Reservable
îî %
,
îî% &
ChargingMechanism
ïï 
=
ïï 
p
ïï  !
.
ïï! "
ChargingMechanism
ïï" 3
,
ïï3 4
LastUpdated
ññ 
=
ññ 
p
ññ 
.
ññ 
LastUpdated
ññ '
}
óó
 
)
óó 
.
óó 
ToList
óó 
(
óó 
)
óó 
}
òò 	
;
òò	 

return
öö 

_mapper
öö 
.
öö 
Map
öö 
<
öö  
ChargingStationDto
öö )
>
öö) *
(
öö* +
await
öö+ 0
query
öö1 6
.
öö6 7!
FirstOrDefaultAsync
öö7 J
(
ööJ K
)
ööK L
.
ööL M
ConfigureAwait
ööM [
(
öö[ \
false
öö\ a
)
ööa b
)
ööb c
;
ööc d
}
õõ 
}úú ë
k/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Repositories/Interface/IBicingStationRepository.cs
	namespace 	
Repositories
 
. 
	Interface  
;  !
public 
	interface $
IBicingStationRepository )
{ 
Task		 
<		 
List		 
<		 
BicingStationDto		 
>		 
>		  
GetAllBicingStations		 3
(		3 4
)		4 5
;		5 6
Task

 
BulkInsertAsync

 
(

 
List

 
<

 
BicingStationEntity

 /
>

/ 0
bicingstations

1 ?
)

? @
;

@ A
Task 
< 
BicingStationDto 
> #
GetBicingStationDetails 0
(0 1
int1 4
id5 7
)7 8
;8 9
} ©2
`/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Repositories/BicingStationRepository.cs
	namespace 	
Repositories
 
; 
public 
class #
BicingStationRepository $
:% &$
IBicingStationRepository' ?
{ 
private 	
readonly
 
ApiDbContext 

_dbContext  *
;* +
private 	
readonly
 
IMapper 
_mapper "
;" #
private 	
readonly
 
ILogger 
< #
BicingStationRepository 2
>2 3
_logger4 ;
;; <
public #
BicingStationRepository	  
(  !
ApiDbContext! -
	dbContext. 7
,7 8
IMapper9 @
mapperA G
,G H
ILoggerI P
<P Q#
BicingStationRepositoryQ h
>h i
loggerj p
)p q
{ 

_dbContext 
= 
	dbContext 
; 
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 
async	 
Task 
BulkInsertAsync #
(# $
List 

<
 
BicingStationEntity 
> 
bicingstations  .
). /
{ 
using 	
(
 
var 
transaction 
= 
await "

_dbContext# -
.- .
Database. 6
.6 7!
BeginTransactionAsync7 L
(L M
)M N
.N O
ConfigureAwaitO ]
(] ^
false^ c
)c d
)d e
{   
try!! 	
{"" 
await## 

_dbContext## 
.## 
Database## !
.##! "
ExecuteSqlRawAsync##" 4
(##4 5
$str##5 \
)##\ ]
.##] ^
ConfigureAwait##^ l
(##l m
false##m r
)##r s
;##s t
await%% 

_dbContext%% 
.%% 
BicingStations%% '
.%%' (
AddRangeAsync%%( 5
(%%5 6
bicingstations%%6 D
)%%D E
.%%E F
ConfigureAwait%%F T
(%%T U
false%%U Z
)%%Z [
;%%[ \
await&& 

_dbContext&& 
.&& 
SaveChangesAsync&& )
(&&) *
)&&* +
.&&+ ,
ConfigureAwait&&, :
(&&: ;
false&&; @
)&&@ A
;&&A B
await(( 
transaction(( 
.(( 
CommitAsync(( %
(((% &
)((& '
.((' (
ConfigureAwait((( 6
(((6 7
false((7 <
)((< =
;((= >
})) 
catch** 
(** 
	Exception** 
)** 
{++ 
await,, 
transaction,, 
.,, 
RollbackAsync,, '
(,,' (
),,( )
.,,) *
ConfigureAwait,,* 8
(,,8 9
false,,9 >
),,> ?
;,,? @
throw-- 
;-- 
}.. 
}// 
}00 
public22 
async22	 
Task22 
<22 
List22 
<22 
BicingStationDto22 )
>22) *
>22* + 
GetAllBicingStations22, @
(22@ A
)22A B
{33 
var44 
entities44 
=44 
await44 

_dbContext44 #
.44# $
BicingStations44$ 2
.55 	
Select55	 
(55 
s55 
=>55 
new55 
BicingStationEntity55 ,
{66 	
BicingId77
 
=77 
s77 
.77 
BicingId77 
,77  

BicingName88
 
=88 
s88 
.88 

BicingName88 #
,88# $
Latitude99
 
=99 
s99 
.99 
Latitude99 
,99  
	Longitude::
 
=:: 
s:: 
.:: 
	Longitude:: !
,::! "
Altitude;;
 
=;; 
s;; 
.;; 
Altitude;; 
,;;  
Address<<
 
=<< 
s<< 
.<< 
Address<< 
,<< 
CrossStreet==
 
=== 
s== 
.== 
CrossStreet== %
,==% &
PostCode>>
 
=>> 
s>> 
.>> 
PostCode>> 
,>>  
Capacity??
 
=?? 
s?? 
.?? 
Capacity?? 
,??  
IsChargingStation@@
 
=@@ 
s@@ 
.@@  
IsChargingStation@@  1
}AA 	
)AA	 

.BB 	
ToListAsyncBB	 
(BB 
)BB 
.BB 
ConfigureAwaitBB %
(BB% &
falseBB& +
)BB+ ,
;BB, -
returnDD 

_mapperDD 
.DD 
MapDD 
<DD 
ListDD 
<DD 
BicingStationDtoDD ,
>DD, -
>DD- .
(DD. /
entitiesDD/ 7
)DD7 8
;DD8 9
}EE 
publicFF 
asyncFF	 
TaskFF 
<FF 
BicingStationDtoFF $
>FF$ %#
GetBicingStationDetailsFF& =
(FF= >
intFF> A
idFFB D
)FFD E
{GG 
varHH 
entityHH 
=HH 
awaitHH 

_dbContextHH !
.HH! "
BicingStationsHH" 0
.II 	
FirstOrDefaultAsyncII	 
(II 
sII 
=>II !
sII" #
.II# $
BicingIdII$ ,
==II- /
idII0 2
)II2 3
.II3 4
ConfigureAwaitII4 B
(IIB C
falseIIC H
)IIH I
;III J
ifKK 
(KK 
entityKK 
==KK 
nullKK 
)KK 
{LL 
returnMM 
nullMM 
;MM 
}NN 
returnPP 

_mapperPP 
.PP 
MapPP 
<PP 
BicingStationDtoPP '
>PP' (
(PP( )
entityPP) /
)PP/ 0
;PP0 1
}QQ 
}RR º
Q/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Mapper/MapperProfiles.cs
	namespace 	
Mapper
 
; 
public 
class 
MapperProfiles 
: 
Profile %
{ 
public 
MapperProfiles	 
( 
) 
{ 
	CreateMap		 
<		 

UserEntity		 
,		 
UserDto		 !
>		! "
(		" #
)		# $
;		$ %
	CreateMap

 
<

 
StationEntity

 
,

 

StationDto

 '
>

' (
(

( )
)

) *
. 	
	ForMember	 
( 
dest 
=> 
dest 
.  
Location  (
,( )
opt* -
=>. 0
opt1 4
.4 5
MapFrom5 <
(< =
src= @
=>A C
srcD G
.G H 
LocationIdNavigationH \
)\ ]
)] ^
;^ _
	CreateMap 
< 
LocationEntity 
, 
LocationDto )
>) *
(* +
)+ ,
;, -
	CreateMap 
< 

HostEntity 
, 
HostDto !
>! "
(" #
)# $
. 	
	ForMember	 
( 
dest 
=> 
dest 
.  
Location  (
,( )
opt* -
=>. 0
opt1 4
.4 5
MapFrom5 <
(< =
src= @
=>A C
srcD G
.G H 
LocationIdNavigationH \
)\ ]
)] ^
;^ _
	CreateMap 
< 

PortEntity 
, 
PortDto !
>! "
(" #
)# $
. 	
	ForMember	 
( 
dest 
=> 
dest 
.  
Station  '
,' (
opt) ,
=>- /
opt0 3
.3 4
MapFrom4 ;
(; <
src< ?
=>@ B
srcC F
.F G
StationIdNavigationG Z
)Z [
)[ \
;\ ]
	CreateMap 
< 
StateBicingEntity 
,  
StateBicingDto! /
>/ 0
(0 1
)1 2
. 	
	ForMember	 
( 
dest 
=> 
dest 
.  
BicingStation  -
,- .
opt/ 2
=>3 5
opt6 9
.9 :
MapFrom: A
(A B
srcB E
=>F H
srcI L
.L M%
BicingStationIdNavigationM f
)f g
)g h
;h i
	CreateMap 
< 
BicingStationEntity !
,! "
BicingStationDto# 3
>3 4
(4 5
)5 6
;6 7
	CreateMap 
<  
SavedUbicationEntity "
," #
SavedUbicationDto$ 5
>5 6
(6 7
)7 8
;8 9
} 
} €
X/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Helpers/PasswordHasherHelper.cs
	namespace 	
Helpers
 
; 
public 
class  
PasswordHasherHelper !
{ 
private		 	
const		
 
int		 
_iterations		 
=		  !
$num		" )
;		) *
private

 	
readonly


 
PasswordHasher

 !
<

! "
UserDto

" )
>

) *
_passwordHasher

+ :
;

: ;
public  
PasswordHasherHelper	 
( 
) 
{ 
var 
hasherOptions 
= 
new !
PasswordHasherOptions 1
{2 3
IterationCount4 B
=C D
_iterationsE P
}Q R
;R S
var 
options 
= 
Options 
. 
Create  
(  !
hasherOptions! .
). /
;/ 0
_passwordHasher 
= 
new 
PasswordHasher (
<( )
UserDto) 0
>0 1
(1 2
options2 9
)9 :
;: ;
} 
public 
string	 
HashPassword 
( 
string #
password$ ,
), -
{ 
return 

_passwordHasher 
. 
HashPassword '
(' (
null( ,
,, -
password. 6
)6 7
;7 8
} 
public &
PasswordVerificationResult	 # 
VerifyHashedPassword$ 8
(8 9
string9 ?
hashedPassword@ N
,N O
stringP V
providedPasswordW g
)g h
{ 
return 

_passwordHasher 
.  
VerifyHashedPassword /
(/ 0
null0 4
,4 5
hashedPassword6 D
,D E
providedPasswordF V
)V W
;W X
} 
} —
c/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Helpers/Interface/IAuthenticationHelper.cs
	namespace 	
Helpers
 
. 
	Interface 
; 
public 
	interface !
IAuthenticationHelper &
{ 
(		 
ClaimsIdentity		 
ClaimsIdentity		  
,		  !$
AuthenticationProperties		" :$
AuthenticationProperties		; S
)		S T 
AuthenticationClaims		U i
(		i j
UserDto		j q
user		r v
)		v w
;		w x
}

 ˝
X/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Helpers/AuthenticationHelper.cs
	namespace 	
Helpers
 
; 
public 
class  
AuthenticationHelper !
:" #!
IAuthenticationHelper$ 9
{ 
public  
AuthenticationHelper	 
( 
) 
{ 
} 
public 
(	 

ClaimsIdentity
 
ClaimsIdentity '
,' ($
AuthenticationProperties) A$
AuthenticationPropertiesB Z
)Z [ 
AuthenticationClaims\ p
(p q
UserDtoq x
usery }
)} ~
{ 
var 
claims 
= 
new 
List 
< 
Claim 
>  
{ 	
new 
Claim 
( 
ApiClaimTypes #
.# $
Name$ (
,( )
user* .
.. /
Username/ 7
)7 8
,8 9
new 
Claim 
( 
ApiClaimTypes #
.# $
Email$ )
,) *
user+ /
./ 0
Email0 5
)5 6
,6 7
new 
Claim 
( 
ApiClaimTypes #
.# $
UserId$ *
,* +
user, 0
.0 1
UserId1 7
.7 8
ToString8 @
(@ A
)A B
)B C
} 	
;	 

var 
claimsIdentity 
= 
new 
ClaimsIdentity +
(+ ,
claims, 2
,2 3(
CookieAuthenticationDefaults4 P
.P Q 
AuthenticationSchemeQ e
)e f
;f g
var 
expirationDate 
= 
DateTimeOffset '
.' (
UtcNow( .
.. /
AddDays/ 6
(6 7
$num7 8
)8 9
;9 :
var $
authenticationProperties  
=! "
new# &$
AuthenticationProperties' ?
{   
IsPersistent!! 
=!! 
true!! 
,!! 

ExpiresUtc"" 
="" 
expirationDate"" !
}## 
;## 
return$$ 

($$ 
claimsIdentity$$ 
,$$ $
authenticationProperties$$ 4
)$$4 5
;$$5 6
}%% 
}&& ‘
^/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/ExtensionMethods/ServiceCollection.cs
	namespace 	
ExtensionMethods
 
; 
public 
static 
class 
ServiceCollection %
{ 
public 
static	 
void 
AddServices  
(  !
this! %
IServiceCollection& 8
services9 A
)A B
{ 
services 
. 
	AddScoped 
< !
IAuthenticationHelper ,
,, - 
AuthenticationHelper. B
>B C
(C D
)D E
;E F
services 
. 
	AddScoped 
< 
IUserService #
,# $
UserService% 0
>0 1
(1 2
)2 3
;3 4
services 
. 
	AddScoped 
< $
IChargingStationsService /
,/ 0#
ChargingStationsService1 H
>H I
(I J
)J K
;K L
services 
. 
	AddScoped 
< 
IStateBicingService *
,* +
StateBicingService, >
>> ?
(? @
)@ A
;A B
services 
. 
	AddScoped 
< !
IBicingStationService ,
,, - 
BicingStationService. B
>B C
(C D
)D E
;E F
services 
. 
	AddScoped 
< 
ITmbService "
," #

TmbService$ .
>. /
(/ 0
)0 1
;1 2
services 
. 
	AddScoped 
< 
IRouteService $
,$ %
RouteService& 2
>2 3
(3 4
)4 5
;5 6
services 
. 
	AddScoped 
< 
IUbicationService (
,( )
UbicationService* :
>: ;
(; <
)< =
;= >
services 
. 
AddHostedService 
< -
!ChargingStationsBackgroundService ?
>? @
(@ A
)A B
;B C
services 
. 
AddHostedService 
< *
BicingStationBackgroundService <
>< =
(= >
)> ?
;? @
services 
. 
AddHostedService 
< (
StateBicingBackgroundService :
>: ;
(; <
)< =
;= >
services"" 
."" 
	AddScoped"" 
<"" 
IUserRepository"" &
,""& '
UserRepository""( 6
>""6 7
(""7 8
)""8 9
;""9 :
services## 
.## 
	AddScoped## 
<## '
IChargingStationsRepository## 2
,##2 3&
ChargingStationsRepository##4 N
>##N O
(##O P
)##P Q
;##Q R
services$$ 
.$$ 
	AddScoped$$ 
<$$ $
IBicingStationRepository$$ /
,$$/ 0#
BicingStationRepository$$1 H
>$$H I
($$I J
)$$J K
;$$K L
services%% 
.%% 
	AddScoped%% 
<%% "
IStateBicingRepository%% -
,%%- .!
StateBicingRepository%%/ D
>%%D E
(%%E F
)%%F G
;%%G H
services&& 
.&& 
	AddScoped&& 
<&& 
IRouteRepository&& '
,&&' (
RouteRepository&&) 8
>&&8 9
(&&9 :
)&&: ;
;&&; <
services'' 
.'' 
	AddScoped'' 
<''  
IUbicationRepository'' +
,''+ ,
UbicationRepository''- @
>''@ A
(''A B
)''B C
;''C D
}(( 
})) À
M/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Entity/UserEntity.cs
	namespace 	
Entity
 
; 
public 
class 

UserEntity 
{ 
public 
Guid	 
UserId 
{ 
get 
; 
set 
;  
}! "
public 
string	 
Username 
{ 
get 
; 
set  #
;# $
}% &
public		 
string			 
Email		 
{		 
get		 
;		 
set		  
;		  !
}		" #
public

 
string

	 
PasswordHash

 
{

 
get

 "
;

" #
set

$ '
;

' (
}

) *
} Â	
P/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Entity/StationEntity.cs
public 
class 
StationEntity 
{ 
public 
required	 
string 
	StationId "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string	 
StationLabel 
{ 
get "
;" #
set$ '
;' (
}) *
public 
required	 
double 
StationLatitude (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
required	 
double 
StationLongitude )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public		 
required			 
string		 

LocationId		 #
{		$ %
get		& )
;		) *
set		+ .
;		. /
}		0 1
public

 
LocationEntity

	 
?

  
LocationIdNavigation

 -
{

. /
get

0 3
;

3 4
set

5 8
;

8 9
}

: ;
} ¡
T/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Entity/StateBicingEntity.cs
	namespace 	
Entity
 
; 
public 
class 
StateBicingEntity 
{ 
public		 
required			 
int		 
BicingId		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
public

 
required

	 
int

 
NumBikesAvailable

 '
{

( )
get

* -
;

- .
set

/ 2
;

2 3
}

4 5
public 
required	 
int '
NumBikesAvailableMechanical 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
required	 
int "
NumBikesAvailableEbike ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
required	 
int 
NumDocksAvailable '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
required	 
DateTime 
LastReported '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
required	 
string 
Status 
{  !
get" %
;% &
set' *
;* +
}, -
public 
virtual	 
BicingStationEntity $%
BicingStationIdNavigation% >
{? @
getA D
;D E
setF I
;I J
}K L
} ≈
W/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Entity/SavedUbicationEntity.cs
	namespace 	
Entity
 
; 
public 
class  
SavedUbicationEntity !
{ 
public		 
required			 
int		 
UbicationId		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
public

 
required

	 
string

 
	UserEmail

 "
{

# $
get

% (
;

( )
set

* -
;

- .
}

/ 0
public 
required	 
string 
StationType $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
required	 
double 
Latitude !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
required	 
double 
	Longitude "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
int	 
? 

Valoration 
{ 
get 
; 
set  #
;# $
}% &
public 
string	 
? 
Comment 
{ 
get 
; 
set  #
;# $
}% &
} À
R/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Entity/RouteUserEntity.cs
public 
class 
RouteUserEntity 
{ 
public 
required	 
Guid 
RutaId 
{ 
get  #
;# $
set% (
;( )
}* +
public 
RouteEntity	 
Ruta 
{ 
get 
;  
set! $
;$ %
}& '
public		 
required			 
Guid		 
	UsuarioId		  
{		! "
get		# &
;		& '
set		( +
;		+ ,
}		- .
public

 

UserEntity

	 
Usuario

 
{

 
get

 !
;

! "
set

# &
;

& '
}

( )
} ‹
N/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Entity/RouteEntity.cs
public 
class 
RouteEntity 
{ 
public		 
required			 
Guid		 
Id		 
{		 
get		 
;		  
set		! $
;		$ %
}		& '
public 
required	 
double 
	OriginLat "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
required	 
double 
	OriginLng "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
required	 
double 
DestinationLat '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
required	 
double 
DestinationLng '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
required	 
string 
Mean 
{ 
get  #
;# $
set% (
;( )
}* +
public 
required	 
string 

Preference #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
required	 
float 
Distance  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
required	 
float 
Duration  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
required	 
string 
GeometryJson %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
required	 
string 
InstructionsJson )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
} à
M/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Entity/PortEntity.cs
public 
class 

PortEntity 
{ 
public 
required	 
string 
PortId 
{  !
get" %
;% &
set' *
;* +
}, -
public 
required	 
string 
ConnectorType &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
required	 
double 
PowerKw  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
required	 
string 
ChargingMechanism *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public		 
required			 
string		 
Status		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
public

 
required

	 
DateTime

 
LastUpdated

 &
{

' (
get

) ,
;

, -
set

. 1
;

1 2
}

3 4
public 
required	 
bool 

Reservable !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
required	 
string 
	StationId "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
virtual	 
StationEntity 
StationIdNavigation 2
{3 4
get5 8
;8 9
set: =
;= >
}? @
} é
Q/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Entity/LocationEntity.cs
public 
class 
LocationEntity 
{ 
public 
required	 
string 

LocationId #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
required	 
string 
NetworkBrandName )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
required	 
string 
OperatorPhone &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
required	 
string 
OperatorWebsite (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public		 
required			 
double		 
Latitude		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
public

 
required

	 
double

 
	Longitude

 "
{

# $
get

% (
;

( )
set

* -
;

- .
}

/ 0
public 
required	 
string 
AddressString &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
required	 
string 
Locality !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
required	 
string 

PostalCode #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} ˆ
M/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Entity/HostEntity.cs
public 
class 

HostEntity 
{ 
public 
required	 
Guid 
HostId 
{ 
get  #
;# $
set% (
;( )
}* +
public 
required	 
string 
HostName !
{" #
get$ '
;' (
set) ,
;, -
}. /
public		 
required			 
string		 
HostAddress		 $
{		% &
get		' *
;		* +
set		, /
;		/ 0
}		1 2
public

 
required

	 
string

 
HostLocality

 %
{

& '
get

( +
;

+ ,
set

- 0
;

0 1
}

2 3
public 
required	 
string 
HostPostalCode '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
required	 
string 
OperatorPhone &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
required	 
string 
OperatorWebsite (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
string	 

LocationId 
{ 
get  
;  !
set" %
;% &
}' (
public 
LocationEntity	 
?  
LocationIdNavigation -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
} ô
V/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Entity/BicingStationEntity.cs
	namespace 	
Entity
 
; 
public 
class 
BicingStationEntity  
{ 
[		 
Key		 
]		 
public 
required	 
int 
BicingId 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
required	 
string 

BicingName #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
required	 
double 
Latitude !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
required	 
double 
	Longitude "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
required	 
double 
Altitude !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
required	 
string 
Address  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
required	 
string 
CrossStreet $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
required	 
string 
PostCode !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
required	 
int 
Capacity 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
required	 
bool 
IsChargingStation (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
} Í°
O/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Entity/ApiDbContext.cs
	namespace 	
Entity
 
; 
public 
class 
ApiDbContext 
: 
	DbContext %
{		 
private

 	
readonly


 
IConfiguration

 !
_configuration

" 0
;

0 1
public 
ApiDbContext	 
( 
DbContextOptions &
<& '
ApiDbContext' 3
>3 4
options5 <
,< =
IConfiguration> L
configurationM Z
)Z [
:\ ]
base^ b
(b c
optionsc j
)j k
{ 
_configuration 
= 
configuration "
;" #
} 
public 
DbSet	 
< 

UserEntity 
> 
Users  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
DbSet	 
< 
LocationEntity 
> 
	Locations (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
DbSet	 
< 

HostEntity 
> 
Hosts  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
DbSet	 
< 
StationEntity 
> 
Stations &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
DbSet	 
< 

PortEntity 
> 
Ports  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
DbSet	 
< 
BicingStationEntity "
>" #
BicingStations$ 2
{3 4
get5 8
;8 9
set: =
;= >
}? @
public 
DbSet	 
< 
StateBicingEntity  
>  !
StateBicing" -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
DbSet	 
<  
SavedUbicationEntity #
># $
SavedUbications% 4
{5 6
get7 :
;: ;
set< ?
;? @
}A B
public 
DbSet	 
< 
RouteEntity 
> 
Routes "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
DbSet	 
< 
RouteUserEntity 
> 

RoutesUser  *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
	protected 
override 
void 
OnConfiguring '
(' (#
DbContextOptionsBuilder( ?
optionsBuilder@ N
)N O
{ 
if   
(   
!   	
optionsBuilder  	 
.   
IsConfigured   $
)  $ %
{!! 
var"" 	
connectionString""
 
="" 
_configuration"" +
.""+ ,
GetConnectionString"", ?
(""? @
$str""@ S
)""S T
;""T U
optionsBuilder## 
.## 
	UseNpgsql## 
(## 
connectionString## /
,##/ 0
options##1 8
=>##9 ;
{$$ 
options%% 
.%% 
UseNetTopologySuite%% #
(%%# $
)%%$ %
;%%% &
options&& 
.&&  
EnableRetryOnFailure&& $
(&&$ %
maxRetryCount'' 
:'' 
$num''  
,''  !
maxRetryDelay(( 
:(( 
TimeSpan(( '
.((' (
FromSeconds((( 3
(((3 4
$num((4 5
)((5 6
,((6 7
errorCodesToAdd)) 
:))  
new))! $
List))% )
<))) *
string))* 0
>))0 1
())1 2
)))2 3
)))3 4
;))4 5
}** 
)** 
;** 	
}++ 
},, 
	protected.. 
override.. 
void.. 
OnModelCreating.. )
(..) *
ModelBuilder..* 6
modelBuilder..7 C
)..C D
{// 
modelBuilder00 
.00  
HasPostgresExtension00 %
(00% &
$str00& /
)00/ 0
;000 1
modelBuilder22 
.22 
Entity22 
<22 

UserEntity22 "
>22" #
(22# $
entity22$ *
=>22+ -
{33 
entity44 
.44 
ToTable44 
(44 
$str44 
)44 
;44 
entity55 
.55 
HasKey55 
(55 
e55 
=>55 
e55 
.55 
UserId55 !
)55! "
;55" #
entity77 
.77 
Property77 
(77 
e77 
=>77 
e77 
.77 
UserId77 #
)77# $
.88 	
HasColumnName88	 
(88 
$str88  
)88  !
.99 	
HasColumnType99	 
(99 
$str99 
)99 
;99 
entity;; 
.;; 
Property;; 
(;; 
e;; 
=>;; 
e;; 
.;; 
Username;; %
);;% &
.<< 	
HasColumnName<<	 
(<< 
$str<< !
)<<! "
.== 	
HasColumnType==	 
(== 
$str== 
)== 
;== 
entity?? 
.?? 
Property?? 
(?? 
e?? 
=>?? 
e?? 
.?? 
Email?? "
)??" #
.@@ 	
HasColumnName@@	 
(@@ 
$str@@ 
)@@ 
.AA 	
HasColumnTypeAA	 
(AA 
$strAA 
)AA 
;AA 
entityCC 
.CC 
PropertyCC 
(CC 
eCC 
=>CC 
eCC 
.CC 
PasswordHashCC )
)CC) *
.DD 	
HasColumnNameDD	 
(DD 
$strDD &
)DD& '
.EE 	
HasColumnTypeEE	 
(EE 
$strEE 
)EE 
;EE 
}FF 
)FF 
;FF 
modelBuilderHH 
.HH 
EntityHH 
<HH 
LocationEntityHH &
>HH& '
(HH' (
entityHH( .
=>HH/ 1
{II 
entityJJ 
.JJ 
ToTableJJ 
(JJ 
$strJJ 
)JJ  
;JJ  !
entityKK 
.KK 
HasKeyKK 
(KK 
eKK 
=>KK 
eKK 
.KK 

LocationIdKK %
)KK% &
;KK& '
entityMM 
.MM 
PropertyMM 
(MM 
eMM 
=>MM 
eMM 
.MM 

LocationIdMM '
)MM' (
.NN 	
HasColumnNameNN	 
(NN 
$strNN $
)NN$ %
.OO 	
HasColumnTypeOO	 
(OO 
$strOO 
)OO 
;OO 
entityQQ 
.QQ 
PropertyQQ 
(QQ 
eQQ 
=>QQ 
eQQ 
.QQ 
NetworkBrandNameQQ -
)QQ- .
.RR 	
HasColumnNameRR	 
(RR 
$strRR +
)RR+ ,
.SS 	
HasColumnTypeSS	 
(SS 
$strSS 
)SS 
;SS 
entityUU 
.UU 
PropertyUU 
(UU 
eUU 
=>UU 
eUU 
.UU 
OperatorPhoneUU *
)UU* +
.VV 	
HasColumnNameVV	 
(VV 
$strVV '
)VV' (
.WW 	
HasColumnTypeWW	 
(WW 
$strWW 
)WW 
;WW 
entityYY 
.YY 
PropertyYY 
(YY 
eYY 
=>YY 
eYY 
.YY 
OperatorWebsiteYY ,
)YY, -
.ZZ 	
HasColumnNameZZ	 
(ZZ 
$strZZ )
)ZZ) *
.[[ 	
HasColumnType[[	 
([[ 
$str[[ 
)[[ 
;[[ 
entity]] 
.]] 
Property]] 
(]] 
e]] 
=>]] 
e]] 
.]] 
Latitude]] %
)]]% &
.^^ 	
HasColumnName^^	 
(^^ 
$str^^ !
)^^! "
.__ 	
HasColumnType__	 
(__ 
$str__ 
)__ 
;__ 
entityaa 
.aa 
Propertyaa 
(aa 
eaa 
=>aa 
eaa 
.aa 
	Longitudeaa &
)aa& '
.bb 	
HasColumnNamebb	 
(bb 
$strbb "
)bb" #
.cc 	
HasColumnTypecc	 
(cc 
$strcc 
)cc 
;cc 
entityee 
.ee 
Propertyee 
(ee 
eee 
=>ee 
eee 
.ee 
AddressStringee *
)ee* +
.ff 	
HasColumnNameff	 
(ff 
$strff '
)ff' (
.gg 	
HasColumnTypegg	 
(gg 
$strgg 
)gg 
;gg 
entityii 
.ii 
Propertyii 
(ii 
eii 
=>ii 
eii 
.ii 
Localityii %
)ii% &
.jj 	
HasColumnNamejj	 
(jj 
$strjj !
)jj! "
.kk 	
HasColumnTypekk	 
(kk 
$strkk 
)kk 
;kk 
entitymm 
.mm 
Propertymm 
(mm 
emm 
=>mm 
emm 
.mm 

PostalCodemm '
)mm' (
.nn 	
HasColumnNamenn	 
(nn 
$strnn $
)nn$ %
.oo 	
HasColumnTypeoo	 
(oo 
$stroo 
)oo 
;oo 
}qq 
)qq 
;qq 
modelBuilderss 
.ss 
Entityss 
<ss 

HostEntityss "
>ss" #
(ss# $
entityss$ *
=>ss+ -
{tt 
entityuu 
.uu 
ToTableuu 
(uu 
$struu 
)uu 
;uu 
entityvv 
.vv 
HasKeyvv 
(vv 
evv 
=>vv 
evv 
.vv 
HostIdvv !
)vv! "
;vv" #
entityxx 
.xx 
Propertyxx 
(xx 
exx 
=>xx 
exx 
.xx 
HostIdxx #
)xx# $
.yy 	
HasColumnNameyy	 
(yy 
$stryy  
)yy  !
.zz 	
HasColumnTypezz	 
(zz 
$strzz 
)zz 
;zz 
entity|| 
.|| 
Property|| 
(|| 
e|| 
=>|| 
e|| 
.|| 
HostName|| %
)||% &
.}} 	
HasColumnName}}	 
(}} 
$str}} "
)}}" #
.~~ 	
HasColumnType~~	 
(~~ 
$str~~ 
)~~ 
;~~ 
entity
ÄÄ 
.
ÄÄ 
Property
ÄÄ 
(
ÄÄ 
e
ÄÄ 
=>
ÄÄ 
e
ÄÄ 
.
ÄÄ 
HostAddress
ÄÄ (
)
ÄÄ( )
.
ÅÅ 	
HasColumnName
ÅÅ	 
(
ÅÅ 
$str
ÅÅ %
)
ÅÅ% &
.
ÇÇ 	
HasColumnType
ÇÇ	 
(
ÇÇ 
$str
ÇÇ 
)
ÇÇ 
;
ÇÇ 
entity
ÑÑ 
.
ÑÑ 
Property
ÑÑ 
(
ÑÑ 
e
ÑÑ 
=>
ÑÑ 
e
ÑÑ 
.
ÑÑ 
HostLocality
ÑÑ )
)
ÑÑ) *
.
ÖÖ 	
HasColumnName
ÖÖ	 
(
ÖÖ 
$str
ÖÖ &
)
ÖÖ& '
.
ÜÜ 	
HasColumnType
ÜÜ	 
(
ÜÜ 
$str
ÜÜ 
)
ÜÜ 
;
ÜÜ 
entity
àà 
.
àà 
Property
àà 
(
àà 
e
àà 
=>
àà 
e
àà 
.
àà 
HostPostalCode
àà +
)
àà+ ,
.
ââ 	
HasColumnName
ââ	 
(
ââ 
$str
ââ )
)
ââ) *
.
ää 	
HasColumnType
ää	 
(
ää 
$str
ää 
)
ää 
;
ää 
entity
åå 
.
åå 
Property
åå 
(
åå 
e
åå 
=>
åå 
e
åå 
.
åå 
OperatorPhone
åå *
)
åå* +
.
çç 	
HasColumnName
çç	 
(
çç 
$str
çç '
)
çç' (
.
éé 	
HasColumnType
éé	 
(
éé 
$str
éé 
)
éé 
;
éé 
entity
êê 
.
êê 
Property
êê 
(
êê 
e
êê 
=>
êê 
e
êê 
.
êê 
OperatorWebsite
êê ,
)
êê, -
.
ëë 	
HasColumnName
ëë	 
(
ëë 
$str
ëë )
)
ëë) *
.
íí 	
HasColumnType
íí	 
(
íí 
$str
íí 
)
íí 
;
íí 
entity
îî 
.
îî 
Property
îî 
(
îî 
e
îî 
=>
îî 
e
îî 
.
îî 

LocationId
îî '
)
îî' (
.
ïï 	
HasColumnName
ïï	 
(
ïï 
$str
ïï $
)
ïï$ %
.
ññ 	
HasColumnType
ññ	 
(
ññ 
$str
ññ 
)
ññ 
;
ññ 
entity
òò 
.
òò 
HasOne
òò 
(
òò 
e
òò 
=>
òò 
e
òò 
.
òò "
LocationIdNavigation
òò /
)
òò/ 0
.
ôô 	
WithMany
ôô	 
(
ôô 
)
ôô 
.
öö 	
HasForeignKey
öö	 
(
öö 
e
öö 
=>
öö 
e
öö 
.
öö 

LocationId
öö (
)
öö( )
;
öö) *
}
úú 
)
úú 
;
úú 
modelBuilder
ûû 
.
ûû 
Entity
ûû 
<
ûû 
StationEntity
ûû %
>
ûû% &
(
ûû& '
entity
ûû' -
=>
ûû. 0
{
üü 
entity
†† 
.
†† 
ToTable
†† 
(
†† 
$str
†† 
)
†† 
;
††  
entity
°° 
.
°° 
HasKey
°° 
(
°° 
e
°° 
=>
°° 
e
°° 
.
°° 
	StationId
°° $
)
°°$ %
;
°°% &
entity
££ 
.
££ 
Property
££ 
(
££ 
e
££ 
=>
££ 
e
££ 
.
££ 
	StationId
££ &
)
££& '
.
§§ 	
HasColumnName
§§	 
(
§§ 
$str
§§ #
)
§§# $
.
•• 	
HasColumnType
••	 
(
•• 
$str
•• 
)
•• 
;
•• 
entity
ßß 
.
ßß 
Property
ßß 
(
ßß 
e
ßß 
=>
ßß 
e
ßß 
.
ßß 
StationLabel
ßß )
)
ßß) *
.
®® 	
HasColumnName
®®	 
(
®® 
$str
®® &
)
®®& '
.
©© 	
HasColumnType
©©	 
(
©© 
$str
©© 
)
©© 
;
©© 
entity
´´ 
.
´´ 
Property
´´ 
(
´´ 
e
´´ 
=>
´´ 
e
´´ 
.
´´ 
StationLatitude
´´ ,
)
´´, -
.
¨¨ 	
HasColumnName
¨¨	 
(
¨¨ 
$str
¨¨ )
)
¨¨) *
.
≠≠ 	
HasColumnType
≠≠	 
(
≠≠ 
$str
≠≠ 
)
≠≠ 
;
≠≠  
entity
ØØ 
.
ØØ 
Property
ØØ 
(
ØØ 
e
ØØ 
=>
ØØ 
e
ØØ 
.
ØØ 
StationLongitude
ØØ -
)
ØØ- .
.
∞∞ 	
HasColumnName
∞∞	 
(
∞∞ 
$str
∞∞ *
)
∞∞* +
.
±± 	
HasColumnType
±±	 
(
±± 
$str
±± 
)
±± 
;
±±  
entity
≥≥ 
.
≥≥ 
Property
≥≥ 
(
≥≥ 
e
≥≥ 
=>
≥≥ 
e
≥≥ 
.
≥≥ 

LocationId
≥≥ '
)
≥≥' (
.
¥¥ 	
HasColumnName
¥¥	 
(
¥¥ 
$str
¥¥ $
)
¥¥$ %
.
µµ 	
HasColumnType
µµ	 
(
µµ 
$str
µµ 
)
µµ 
;
µµ 
entity
∑∑ 
.
∑∑ 
HasOne
∑∑ 
(
∑∑ 
e
∑∑ 
=>
∑∑ 
e
∑∑ 
.
∑∑ "
LocationIdNavigation
∑∑ /
)
∑∑/ 0
.
∏∏ 	
WithMany
∏∏	 
(
∏∏ 
)
∏∏ 
.
ππ 	
HasForeignKey
ππ	 
(
ππ 
e
ππ 
=>
ππ 
e
ππ 
.
ππ 

LocationId
ππ (
)
ππ( )
;
ππ) *
}
ªª 
)
ªª 
;
ªª 
modelBuilder
ΩΩ 
.
ΩΩ 
Entity
ΩΩ 
<
ΩΩ 

PortEntity
ΩΩ "
>
ΩΩ" #
(
ΩΩ# $
entity
ΩΩ$ *
=>
ΩΩ+ -
{
ææ 
entity
øø 
.
øø 
ToTable
øø 
(
øø 
$str
øø 
)
øø 
;
øø 
entity
¿¿ 
.
¿¿ 
HasKey
¿¿ 
(
¿¿ 
e
¿¿ 
=>
¿¿ 
new
¿¿ 
{
¿¿ 
e
¿¿  
.
¿¿  !
	StationId
¿¿! *
,
¿¿* +
e
¿¿, -
.
¿¿- .
PortId
¿¿. 4
}
¿¿5 6
)
¿¿6 7
;
¿¿7 8
entity
¬¬ 
.
¬¬ 
Property
¬¬ 
(
¬¬ 
e
¬¬ 
=>
¬¬ 
e
¬¬ 
.
¬¬ 
PortId
¬¬ #
)
¬¬# $
.
√√ 	
HasColumnName
√√	 
(
√√ 
$str
√√  
)
√√  !
.
ƒƒ 	
HasColumnType
ƒƒ	 
(
ƒƒ 
$str
ƒƒ 
)
ƒƒ 
;
ƒƒ 
entity
∆∆ 
.
∆∆ 
Property
∆∆ 
(
∆∆ 
e
∆∆ 
=>
∆∆ 
e
∆∆ 
.
∆∆ 
ConnectorType
∆∆ *
)
∆∆* +
.
«« 	
HasColumnName
««	 
(
«« 
$str
«« '
)
««' (
.
»» 	
HasColumnType
»»	 
(
»» 
$str
»» 
)
»» 
;
»» 
entity
   
.
   
Property
   
(
   
e
   
=>
   
e
   
.
   
PowerKw
   $
)
  $ %
.
ÀÀ 	
HasColumnName
ÀÀ	 
(
ÀÀ 
$str
ÀÀ !
)
ÀÀ! "
.
ÃÃ 	
HasColumnType
ÃÃ	 
(
ÃÃ 
$str
ÃÃ 
)
ÃÃ 
;
ÃÃ  
entity
ŒŒ 
.
ŒŒ 
Property
ŒŒ 
(
ŒŒ 
e
ŒŒ 
=>
ŒŒ 
e
ŒŒ 
.
ŒŒ 
ChargingMechanism
ŒŒ .
)
ŒŒ. /
.
œœ 	
HasColumnName
œœ	 
(
œœ 
$str
œœ +
)
œœ+ ,
.
–– 	
HasColumnType
––	 
(
–– 
$str
–– 
)
–– 
;
–– 
entity
““ 
.
““ 
Property
““ 
(
““ 
e
““ 
=>
““ 
e
““ 
.
““ 
Status
““ #
)
““# $
.
”” 	
HasColumnName
””	 
(
”” 
$str
”” 
)
””  
.
‘‘ 	
HasColumnType
‘‘	 
(
‘‘ 
$str
‘‘ 
)
‘‘ 
;
‘‘ 
entity
÷÷ 
.
÷÷ 
Property
÷÷ 
(
÷÷ 
e
÷÷ 
=>
÷÷ 
e
÷÷ 
.
÷÷ 

Reservable
÷÷ '
)
÷÷' (
.
◊◊ 	
HasColumnName
◊◊	 
(
◊◊ 
$str
◊◊ #
)
◊◊# $
.
ÿÿ 	
HasColumnType
ÿÿ	 
(
ÿÿ 
$str
ÿÿ  
)
ÿÿ  !
;
ÿÿ! "
entity
⁄⁄ 
.
⁄⁄ 
Property
⁄⁄ 
(
⁄⁄ 
e
⁄⁄ 
=>
⁄⁄ 
e
⁄⁄ 
.
⁄⁄ 
LastUpdated
⁄⁄ (
)
⁄⁄( )
.
€€ 	
HasColumnName
€€	 
(
€€ 
$str
€€ %
)
€€% &
.
‹‹ 	
HasColumnType
‹‹	 
(
‹‹ 
$str
‹‹ "
)
‹‹" #
;
‹‹# $
entity
ﬁﬁ 
.
ﬁﬁ 
Property
ﬁﬁ 
(
ﬁﬁ 
e
ﬁﬁ 
=>
ﬁﬁ 
e
ﬁﬁ 
.
ﬁﬁ 
	StationId
ﬁﬁ &
)
ﬁﬁ& '
.
ﬂﬂ 	
HasColumnName
ﬂﬂ	 
(
ﬂﬂ 
$str
ﬂﬂ #
)
ﬂﬂ# $
.
‡‡ 	
HasColumnType
‡‡	 
(
‡‡ 
$str
‡‡ 
)
‡‡ 
;
‡‡ 
entity
‚‚ 
.
‚‚ 
HasOne
‚‚ 
(
‚‚ 
e
‚‚ 
=>
‚‚ 
e
‚‚ 
.
‚‚ !
StationIdNavigation
‚‚ .
)
‚‚. /
.
„„ 	
WithMany
„„	 
(
„„ 
)
„„ 
.
‰‰ 	
HasForeignKey
‰‰	 
(
‰‰ 
e
‰‰ 
=>
‰‰ 
e
‰‰ 
.
‰‰ 
	StationId
‰‰ '
)
‰‰' (
;
‰‰( )
}
ÊÊ 
)
ÊÊ 
;
ÊÊ 
modelBuilder
ËË 
.
ËË 
Entity
ËË 
<
ËË !
BicingStationEntity
ËË +
>
ËË+ ,
(
ËË, -
entity
ËË- 3
=>
ËË4 6
{
ÈÈ 
entity
ÍÍ 
.
ÍÍ 
ToTable
ÍÍ 
(
ÍÍ 
$str
ÍÍ %
)
ÍÍ% &
;
ÍÍ& '
entity
ÎÎ 
.
ÎÎ 
HasKey
ÎÎ 
(
ÎÎ 
e
ÎÎ 
=>
ÎÎ 
e
ÎÎ 
.
ÎÎ 
BicingId
ÎÎ #
)
ÎÎ# $
;
ÎÎ$ %
entity
ÌÌ 
.
ÌÌ 
Property
ÌÌ 
(
ÌÌ 
e
ÌÌ 
=>
ÌÌ 
e
ÌÌ 
.
ÌÌ 
BicingId
ÌÌ %
)
ÌÌ% &
.
ÓÓ 	
HasColumnName
ÓÓ	 
(
ÓÓ 
$str
ÓÓ "
)
ÓÓ" #
.
ÔÔ 	
HasColumnType
ÔÔ	 
(
ÔÔ 
$str
ÔÔ  
)
ÔÔ  !
;
ÔÔ! "
entity
ÒÒ 
.
ÒÒ 
Property
ÒÒ 
(
ÒÒ 
e
ÒÒ 
=>
ÒÒ 
e
ÒÒ 
.
ÒÒ 

BicingName
ÒÒ '
)
ÒÒ' (
.
ÚÚ 	
HasColumnName
ÚÚ	 
(
ÚÚ 
$str
ÚÚ $
)
ÚÚ$ %
.
ÛÛ 	
HasColumnType
ÛÛ	 
(
ÛÛ 
$str
ÛÛ "
)
ÛÛ" #
;
ÛÛ# $
entity
ıı 
.
ıı 
Property
ıı 
(
ıı 
e
ıı 
=>
ıı 
e
ıı 
.
ıı 
Latitude
ıı %
)
ıı% &
.
ˆˆ 	
HasColumnName
ˆˆ	 
(
ˆˆ 
$str
ˆˆ !
)
ˆˆ! "
.
˜˜ 	
HasColumnType
˜˜	 
(
˜˜ 
$str
˜˜ 
)
˜˜ 
;
˜˜ 
entity
˘˘ 
.
˘˘ 
Property
˘˘ 
(
˘˘ 
e
˘˘ 
=>
˘˘ 
e
˘˘ 
.
˘˘ 
	Longitude
˘˘ &
)
˘˘& '
.
˙˙ 	
HasColumnName
˙˙	 
(
˙˙ 
$str
˙˙ "
)
˙˙" #
.
˚˚ 	
HasColumnType
˚˚	 
(
˚˚ 
$str
˚˚ 
)
˚˚ 
;
˚˚ 
entity
˝˝ 
.
˝˝ 
Property
˝˝ 
(
˝˝ 
e
˝˝ 
=>
˝˝ 
e
˝˝ 
.
˝˝ 
Altitude
˝˝ %
)
˝˝% &
.
˛˛ 	
HasColumnName
˛˛	 
(
˛˛ 
$str
˛˛ !
)
˛˛! "
.
ˇˇ 	
HasColumnType
ˇˇ	 
(
ˇˇ 
$str
ˇˇ 
)
ˇˇ 
;
ˇˇ 
entity
ÅÅ 
.
ÅÅ 
Property
ÅÅ 
(
ÅÅ 
e
ÅÅ 
=>
ÅÅ 
e
ÅÅ 
.
ÅÅ 
Address
ÅÅ $
)
ÅÅ$ %
.
ÇÇ 	
HasColumnName
ÇÇ	 
(
ÇÇ 
$str
ÇÇ  
)
ÇÇ  !
.
ÉÉ 	
HasColumnType
ÉÉ	 
(
ÉÉ 
$str
ÉÉ "
)
ÉÉ" #
;
ÉÉ# $
entity
ÖÖ 
.
ÖÖ 
Property
ÖÖ 
(
ÖÖ 
e
ÖÖ 
=>
ÖÖ 
e
ÖÖ 
.
ÖÖ 
CrossStreet
ÖÖ (
)
ÖÖ( )
.
ÜÜ 	
HasColumnName
ÜÜ	 
(
ÜÜ 
$str
ÜÜ %
)
ÜÜ% &
.
áá 	
HasColumnType
áá	 
(
áá 
$str
áá "
)
áá" #
;
áá# $
entity
ââ 
.
ââ 
Property
ââ 
(
ââ 
e
ââ 
=>
ââ 
e
ââ 
.
ââ 
PostCode
ââ %
)
ââ% &
.
ää 	
HasColumnName
ää	 
(
ää 
$str
ää "
)
ää" #
.
ãã 	
HasColumnType
ãã	 
(
ãã 
$str
ãã !
)
ãã! "
;
ãã" #
entity
çç 
.
çç 
Property
çç 
(
çç 
e
çç 
=>
çç 
e
çç 
.
çç 
Capacity
çç %
)
çç% &
.
éé 	
HasColumnName
éé	 
(
éé 
$str
éé !
)
éé! "
.
èè 	
HasColumnType
èè	 
(
èè 
$str
èè  
)
èè  !
;
èè! "
entity
ëë 
.
ëë 
Property
ëë 
(
ëë 
e
ëë 
=>
ëë 
e
ëë 
.
ëë 
IsChargingStation
ëë .
)
ëë. /
.
íí 	
HasColumnName
íí	 
(
íí 
$str
íí ,
)
íí, -
.
ìì 	
HasColumnType
ìì	 
(
ìì 
$str
ìì  
)
ìì  !
;
ìì! "
}
ïï 
)
ïï 
;
ïï 
modelBuilder
óó 
.
óó 
Entity
óó 
<
óó 
StateBicingEntity
óó )
>
óó) *
(
óó* +
entity
óó+ 1
=>
óó2 4
{
òò 
entity
ôô 
.
ôô 
ToTable
ôô 
(
ôô 
$str
ôô #
)
ôô# $
;
ôô$ %
entity
öö 
.
öö 
HasKey
öö 
(
öö 
e
öö 
=>
öö 
e
öö 
.
öö 
BicingId
öö #
)
öö# $
;
öö$ %
entity
úú 
.
úú 
Property
úú 
(
úú 
e
úú 
=>
úú 
e
úú 
.
úú 
BicingId
úú %
)
úú% &
.
ùù 	
HasColumnName
ùù	 
(
ùù 
$str
ùù "
)
ùù" #
.
ûû 	
HasColumnType
ûû	 
(
ûû 
$str
ûû  
)
ûû  !
;
ûû! "
entity
†† 
.
†† 
Property
†† 
(
†† 
e
†† 
=>
†† 
e
†† 
.
†† 
NumBikesAvailable
†† .
)
††. /
.
°° 	
HasColumnName
°°	 
(
°° 
$str
°° ,
)
°°, -
.
¢¢ 	
HasColumnType
¢¢	 
(
¢¢ 
$str
¢¢  
)
¢¢  !
;
¢¢! "
entity
§§ 
.
§§ 
Property
§§ 
(
§§ 
e
§§ 
=>
§§ 
e
§§ 
.
§§ )
NumBikesAvailableMechanical
§§ 8
)
§§8 9
.
•• 	
HasColumnName
••	 
(
•• 
$str
•• 7
)
••7 8
.
¶¶ 	
HasColumnType
¶¶	 
(
¶¶ 
$str
¶¶  
)
¶¶  !
;
¶¶! "
entity
®® 
.
®® 
Property
®® 
(
®® 
e
®® 
=>
®® 
e
®® 
.
®® $
NumBikesAvailableEbike
®® 3
)
®®3 4
.
©© 	
HasColumnName
©©	 
(
©© 
$str
©© 2
)
©©2 3
.
™™ 	
HasColumnType
™™	 
(
™™ 
$str
™™  
)
™™  !
;
™™! "
entity
¨¨ 
.
¨¨ 
Property
¨¨ 
(
¨¨ 
e
¨¨ 
=>
¨¨ 
e
¨¨ 
.
¨¨ 
NumDocksAvailable
¨¨ .
)
¨¨. /
.
≠≠ 	
HasColumnName
≠≠	 
(
≠≠ 
$str
≠≠ ,
)
≠≠, -
.
ÆÆ 	
HasColumnType
ÆÆ	 
(
ÆÆ 
$str
ÆÆ  
)
ÆÆ  !
;
ÆÆ! "
entity
∞∞ 
.
∞∞ 
Property
∞∞ 
(
∞∞ 
e
∞∞ 
=>
∞∞ 
e
∞∞ 
.
∞∞ 
LastReported
∞∞ )
)
∞∞) *
.
±± 	
HasColumnName
±±	 
(
±± 
$str
±± &
)
±±& '
.
≤≤ 	
HasColumnType
≤≤	 
(
≤≤ 
$str
≤≤ 
)
≤≤ 
;
≤≤ 
entity
¥¥ 
.
¥¥ 
Property
¥¥ 
(
¥¥ 
e
¥¥ 
=>
¥¥ 
e
¥¥ 
.
¥¥ 
Status
¥¥ #
)
¥¥# $
.
µµ 	
HasColumnName
µµ	 
(
µµ 
$str
µµ 
)
µµ  
.
∂∂ 	
HasColumnType
∂∂	 
(
∂∂ 
$str
∂∂ !
)
∂∂! "
;
∂∂" #
entity
∏∏ 
.
∏∏ 
HasOne
∏∏ 
(
∏∏ 
e
∏∏ 
=>
∏∏ 
e
∏∏ 
.
∏∏ '
BicingStationIdNavigation
∏∏ 4
)
∏∏4 5
.
ππ 	
WithMany
ππ	 
(
ππ 
)
ππ 
.
∫∫ 	
HasForeignKey
∫∫	 
(
∫∫ 
e
∫∫ 
=>
∫∫ 
e
∫∫ 
.
∫∫ 
BicingId
∫∫ &
)
∫∫& '
;
∫∫' (
}
ªª 
)
ªª 
;
ªª 
modelBuilder
ΩΩ 
.
ΩΩ 
Entity
ΩΩ 
<
ΩΩ "
SavedUbicationEntity
ΩΩ ,
>
ΩΩ, -
(
ΩΩ- .
entity
ΩΩ. 4
=>
ΩΩ5 7
{
ææ 
entity
øø 
.
øø 
ToTable
øø 
(
øø 
$str
øø )
)
øø) *
;
øø* +
entity
¡¡ 
.
¡¡ 
HasKey
¡¡ 
(
¡¡ 
e
¡¡ 
=>
¡¡ 
new
¡¡ 
{
¡¡  
e
¡¡! "
.
¡¡" #
UbicationId
¡¡# .
,
¡¡. /
e
¡¡0 1
.
¡¡1 2
	UserEmail
¡¡2 ;
,
¡¡; <
e
¡¡= >
.
¡¡> ?
StationType
¡¡? J
}
¡¡K L
)
¡¡L M
;
¡¡M N
entity
ƒƒ 
.
ƒƒ 
Property
ƒƒ 
(
ƒƒ 
e
ƒƒ 
=>
ƒƒ 
e
ƒƒ 
.
ƒƒ 
UbicationId
ƒƒ *
)
ƒƒ* +
.
≈≈ 
HasColumnName
≈≈ 
(
≈≈ 
$str
≈≈ +
)
≈≈+ ,
.
∆∆ 
HasColumnType
∆∆ 
(
∆∆ 
$str
∆∆ &
)
∆∆& '
;
∆∆' (
entity
»» 
.
»» 
Property
»» 
(
»» 
e
»» 
=>
»» 
e
»» 
.
»» 
	UserEmail
»» (
)
»»( )
.
…… 
HasColumnName
…… 
(
…… 
$str
…… $
)
……$ %
.
   
HasColumnType
   
(
   
$str
   #
)
  # $
;
  $ %
entity
ÃÃ 
.
ÃÃ 
Property
ÃÃ 
(
ÃÃ 
e
ÃÃ 
=>
ÃÃ 
e
ÃÃ 
.
ÃÃ 
StationType
ÃÃ *
)
ÃÃ* +
.
ÕÕ 
HasColumnName
ÕÕ 
(
ÕÕ 
$str
ÕÕ +
)
ÕÕ+ ,
.
ŒŒ 
HasColumnType
ŒŒ 
(
ŒŒ 
$str
ŒŒ #
)
ŒŒ# $
;
ŒŒ$ %
entity
–– 
.
–– 
Property
–– 
(
–– 
e
–– 
=>
–– 
e
–– 
.
–– 
Latitude
–– '
)
––' (
.
—— 
HasColumnName
—— 
(
—— 
$str
—— '
)
——' (
.
““ 
HasColumnType
““ 
(
““ 
$str
““ #
)
““# $
;
““$ %
entity
‘‘ 
.
‘‘ 
Property
‘‘ 
(
‘‘ 
e
‘‘ 
=>
‘‘ 
e
‘‘ 
.
‘‘ 
	Longitude
‘‘ (
)
‘‘( )
.
’’ 
HasColumnName
’’ 
(
’’ 
$str
’’ &
)
’’& '
.
÷÷ 
HasColumnType
÷÷ 
(
÷÷ 
$str
÷÷ !
)
÷÷! "
;
÷÷" #
entity
ÿÿ 
.
ÿÿ 
Property
ÿÿ 
(
ÿÿ 
e
ÿÿ 
=>
ÿÿ 
e
ÿÿ 
.
ÿÿ 

Valoration
ÿÿ )
)
ÿÿ) *
.
ŸŸ 
HasColumnName
ŸŸ 
(
ŸŸ 
$str
ŸŸ )
)
ŸŸ) *
.
⁄⁄ 
HasColumnType
⁄⁄ 
(
⁄⁄ 
$str
⁄⁄ &
)
⁄⁄& '
;
⁄⁄' (
entity
‹‹ 
.
‹‹ 
Property
‹‹ 
(
‹‹ 
e
‹‹ 
=>
‹‹ 
e
‹‹ 
.
‹‹ 
Comment
‹‹ &
)
‹‹& '
.
›› 
HasColumnName
›› 
(
›› 
$str
›› &
)
››& '
.
ﬁﬁ 
HasColumnType
ﬁﬁ 
(
ﬁﬁ 
$str
ﬁﬁ #
)
ﬁﬁ# $
;
ﬁﬁ$ %
}
ﬂﬂ 
)
ﬂﬂ 
;
ﬂﬂ 	
modelBuilder
·· 
.
·· 
Entity
·· 
<
·· 
RouteUserEntity
·· '
>
··' (
(
··( )
entity
··) /
=>
··0 2
{
‚‚ 
entity
„„ 
.
„„ 
ToTable
„„ 
(
„„ 
$str
„„ #
)
„„# $
;
„„$ %
entity
‰‰ 
.
‰‰ 
HasKey
‰‰ 
(
‰‰ 
e
‰‰ 
=>
‰‰ 
new
‰‰ 
{
‰‰  
e
‰‰! "
.
‰‰" #
	UsuarioId
‰‰# ,
,
‰‰, -
e
‰‰. /
.
‰‰/ 0
RutaId
‰‰0 6
}
‰‰7 8
)
‰‰8 9
;
‰‰9 :
entity
ÊÊ 
.
ÊÊ 
Property
ÊÊ 
(
ÊÊ 
e
ÊÊ 
=>
ÊÊ 
e
ÊÊ 
.
ÊÊ 
	UsuarioId
ÊÊ (
)
ÊÊ( )
.
ÁÁ
 
HasColumnName
ÁÁ 
(
ÁÁ 
$str
ÁÁ !
)
ÁÁ! "
.
ËË
 
HasColumnType
ËË 
(
ËË 
$str
ËË 
)
ËË  
;
ËË  !
entity
ÍÍ 
.
ÍÍ 
Property
ÍÍ 
(
ÍÍ 
e
ÍÍ 
=>
ÍÍ 
e
ÍÍ 
.
ÍÍ 
RutaId
ÍÍ %
)
ÍÍ% &
.
ÎÎ
 
HasColumnName
ÎÎ 
(
ÎÎ 
$str
ÎÎ "
)
ÎÎ" #
.
ÏÏ
 
HasColumnType
ÏÏ 
(
ÏÏ 
$str
ÏÏ 
)
ÏÏ  
;
ÏÏ  !
entity
ÓÓ 
.
ÓÓ 
HasOne
ÓÓ 
(
ÓÓ 
e
ÓÓ 
=>
ÓÓ 
e
ÓÓ 
.
ÓÓ 
Ruta
ÓÓ !
)
ÓÓ! "
.
ÔÔ
 
WithMany
ÔÔ 
(
ÔÔ 
)
ÔÔ 
.

 
HasForeignKey
 
(
 
e
 
=>
 
e
 
.
  
RutaId
  &
)
& '
;
' (
entity
ÚÚ 
.
ÚÚ 
HasOne
ÚÚ 
(
ÚÚ 
e
ÚÚ 
=>
ÚÚ 
e
ÚÚ 
.
ÚÚ 
Usuario
ÚÚ $
)
ÚÚ$ %
.
ÛÛ
 
WithMany
ÛÛ 
(
ÛÛ 
)
ÛÛ 
.
ÙÙ
 
HasForeignKey
ÙÙ 
(
ÙÙ 
e
ÙÙ 
=>
ÙÙ 
e
ÙÙ 
.
ÙÙ  
	UsuarioId
ÙÙ  )
)
ÙÙ) *
;
ÙÙ* +
}
ˆˆ 
)
ˆˆ 
;
ˆˆ 	
}
˜˜ 
}¯¯ ô
G/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/UserDto.cs
	namespace 	
Dto
 
; 
public 
class 
UserDto 
{ 
public 
required	 
string 
UserId 
{  !
get" %
;% &
set' *
;* +
}, -
public 
required	 
string 
Username !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
required	 
string 
Email 
{  
get! $
;$ %
set& )
;) *
}+ ,
public		 
required			 
string		 
PasswordHash		 %
{		& '
get		( +
;		+ ,
set		- 0
;		0 1
}		2 3
}

 «
O/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/UserCredentials.cs
	namespace 	
Dto
 
; 
public 
class 
UserCredentials 
{ 
public 
required	 
string 
	UserEmail "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
required	 
string 
Password !
{" #
get$ '
;' (
set) ,
;, -
}. /
} Ô
J/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/UserCreate.cs
	namespace 	
Dto
 
; 
public 
class 

UserCreate 
{ 
public 
required	 
string 
Username !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
required	 
string 
Email 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
required	 
string 
PasswordHash %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} Ÿ
W/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/UbicationInfoRequestDto.cs
	namespace 	
Dto
 
; 
public 
class #
UbicationInfoRequestDto $
{ 
public 
required	 
int 
UbicationId !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
required	 
string 
StationType $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} —
P/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/UbicationInfoDto.cs
	namespace 	
Dto
 
; 
public 
class 
UbicationInfoDto 
{ 
public 
required	 
int 
UbicationId !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
required	 
string 
Username !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
required	 
string 
StationType $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
int	 
? 

Valoration 
{ 
get 
; 
set  #
;# $
}% &
public 
string	 
? 
Comment 
{ 
get 
; 
set  #
;# $
}% &
}		 å
J/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/StationDto.cs
public 
class 

StationDto 
{ 
[ 
JsonPropertyName 
( 
$str 
) 
] 
public 
required	 
string 
	StationId "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 
JsonPropertyName 
( 
$str 
) 
] 
public		 
string			 
?		 
StationLabel		 
{		 
get		  #
;		# $
set		% (
;		( )
}		* +
[

 
JsonPropertyName

 
(

 
$str

 *
)

* +
]

+ ,
public 
required	 
double 
StationLatitude (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
[ 
JsonPropertyName 
( 
$str +
)+ ,
], -
public 
required	 
double 
StationLongitude )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
required	 
string 

LocationId #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
LocationDto	 
? 
Location 
{  
get! $
;$ %
set& )
;) *
}+ ,
} î
N/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/StateBicingDto.cs
	namespace 	
Dto
 
; 
public 
class 
StateBicingDto 
{ 
[		 
JsonPropertyName		 
(		 
$str		  
)		  !
]		! "
public

 
required

	 
int

 
BicingId

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
[ 
JsonPropertyName 
( 
$str )
)) *
]* +
public 
required	 
int 
NumBikesAvailable '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
[ 
JsonPropertyName 
( 
$str :
): ;
]; <
public 
required	 
int '
NumBikesAvailableMechanical 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
[ 
JsonPropertyName 
( 
$str 5
)5 6
]6 7
public 
required	 
int "
NumBikesAvailableEbike ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
[ 
JsonPropertyName 
( 
$str )
)) *
]* +
public 
required	 
int 
NumDocksAvailable '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
[ 
JsonPropertyName 
( 
$str #
)# $
]$ %
public 
required	 
DateTime 
LastReported '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
[ 
JsonPropertyName 
( 
$str 
) 
] 
public 
required	 
string 
Status 
{  !
get" %
;% &
set' *
;* +
}, -
public 
BicingStationDto	 
? 
BicingStation (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
} ’
Q/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/SavedUbicationDto.cs
	namespace 	
Dto
 
; 
public 
class 
SavedUbicationDto 
{ 
[ 
JsonPropertyName 
( 
$str "
)" #
]# $
public		 
required			 
int		 
UbicationId		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
[ 
JsonPropertyName 
( 
$str 
) 
]  
public 
required	 
string 
	UserEmail "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 
JsonPropertyName 
( 
$str "
)" #
]# $
public 
required	 
string 
StationType $
{% &
get' *
;* +
set, /
;/ 0
}1 2
[ 
JsonPropertyName 
( 
$str 
) 
]  
public 
required	 
double 
Latitude !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 
JsonPropertyName 
( 
$str 
)  
]  !
public 
required	 
double 
	Longitude "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
int	 
? 

Valoration 
{ 
get 
; 
set  #
;# $
}% &
public 
string	 
? 
Comment 
{ 
get 
; 
set  #
;# $
}% &
} ˚
P/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/RouteResponseDto.cs
public 
class 
RouteResponseDto 
{ 
public 
double	 
Distance 
{ 
get 
; 
set  #
;# $
}% &
public 
double	 
Duration 
{ 
get 
; 
set  #
;# $
}% &
public 
List	 
< 
double 
[ 
] 
> 
Geometry  
{! "
get# &
;& '
set( +
;+ ,
}- .
=/ 0
new1 4
(4 5
)5 6
;6 7
public 
List	 
< 
RouteInstructionDto !
>! "
Instructions# /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
}		 ë	
O/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/RouteRequestDto.cs
public 
class 
RouteRequestDto 
{ 
public 
double	 
	OriginLat 
{ 
get 
;  
set! $
;$ %
}& '
public 
double	 
	OriginLng 
{ 
get 
;  
set! $
;$ %
}& '
public 
double	 
DestinationLat 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
double	 
DestinationLng 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string	 
Mode 
{ 
get 
; 
set 
;  
}! "
public 
string	 

Preference 
{ 
get  
;  !
set" %
;% &
}' (
=) *
$str+ 4
;4 5
}		 à
S/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/RouteInstructionDto.cs
public 
class 
RouteInstructionDto  
{ 
public 
string	 
Instruction 
{ 
get !
;! "
set# &
;& '
}( )
public 
double	 
Distance 
{ 
get 
; 
set  #
;# $
}% &
public 
string	 
Mode 
{ 
get 
; 
set 
;  
}! "
} Ë
G/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/PortDto.cs
public 
class 
PortDto 
{ 
[ 
JsonPropertyName 
( 
$str 
) 
] 
public 
required	 
string 
PortId 
{  !
get" %
;% &
set' *
;* +
}, -
[

 
JsonPropertyName

 
(

 
$str

 $
)

$ %
]

% &
public 
required	 
string 
ConnectorType &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
[ 
JsonPropertyName 
( 
$str 
) 
]  
public 
required	 
double 
PowerKw  
{! "
get# &
;& '
set( +
;+ ,
}- .
[ 
JsonPropertyName 
( 
$str (
)( )
]) *
public 
required	 
string 
ChargingMechanism *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
[ 
JsonPropertyName 
( 
$str +
)+ ,
], -
public 
required	 
string 

PortStatus #
{$ %
get& )
;) *
set+ .
;. /
}0 1
[ 
JsonPropertyName 
( 
$str  
)  !
]! "
public 
required	 
bool 

Reservable !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 
JsonPropertyName 
( 
$str "
)" #
]# $
public 
required	 
DateTime 
LastUpdated &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
required	 
string 
	StationId "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
virtual	 

StationDto 
Station #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} ¡
N/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/LoginGoogleDto.cs
	namespace 	
Dto
 
; 
public 
class 
LoginGoogleDto 
{ 
public 
required	 
string 
Email 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
required	 
string 
Username !
{" #
get$ '
;' (
set) ,
;, -
}. /
} ∂
H/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/MetroDto.cs
	namespace 	
Dto
 
; 
public 
class 
MetroDto 
{ 
public 
int	 
IdEstacioLinia 
{ 
get !
;! "
set# &
;& '
}( )
public 
int	 
CodiEstacioLinia 
{ 
get  #
;# $
set% (
;( )
}* +
public 
int	 
IdGrupEstacio 
{ 
get  
;  !
set" %
;% &
}' (
public 
int	 
CodiGrupEstacio 
{ 
get "
;" #
set$ '
;' (
}) *
public		 
int			 
	IdEstacio		 
{		 
get		 
;		 
set		 !
;		! "
}		# $
public

 
int

	 
CodiEstacio

 
{

 
get

 
;

 
set

  #
;

# $
}

% &
public 
string	 

NomEstacio 
{ 
get  
;  !
set" %
;% &
}' (
public 
int	 
OrdreEstacio 
{ 
get 
;  
set! $
;$ %
}& '
public 
int	 
IdLinia 
{ 
get 
; 
set 
;  
}! "
public 
int	 
	CodiLinia 
{ 
get 
; 
set !
;! "
}# $
public 
string	 
NomLinia 
{ 
get 
; 
set  #
;# $
}% &
public 
string	 

DescServei 
{ 
get  
;  !
set" %
;% &
}' (
public 
string	 
OrigenServei 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string	 
DestiServei 
{ 
get !
;! "
set# &
;& '
}( )
public 
string	 

ColorLinia 
{ 
get  
;  !
set" %
;% &
}' (
public 
string	 
Picto 
{ 
get 
; 
set  
;  !
}" #
public 
string	 
	PictoGrup 
{ 
get 
;  
set! $
;$ %
}& '
public 
string	 
DataInauguracio 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string	 
Data 
{ 
get 
; 
set 
;  
}! "
public 
double	 
Latitude 
{ 
get 
; 
set  #
;# $
}% &
public 
double	 
	Longitude 
{ 
get 
;  
set! $
;$ %
}& '
} •
K/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/LocationDto.cs
public 
class 
LocationDto 
{ 
[ 
JsonPropertyName 
( 
$str 
) 
] 
public 
required	 
string 

LocationId #
{$ %
get& )
;) *
set+ .
;. /
}0 1
[ 
JsonPropertyName 
( 
$str (
)( )
]) *
public		 
required			 
string		 
NetworkBrandName		 )
{		* +
get		, /
;		/ 0
set		1 4
;		4 5
}		6 7
[ 
JsonPropertyName 
( 
$str ,
), -
]- .
public 
required	 
string 
OperatorPhone &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
[ 
JsonPropertyName 
( 
$str .
). /
]/ 0
public 
required	 
string 
OperatorWebsite (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
[ 
JsonPropertyName 
( 
$str *
)* +
]+ ,
public 
double	 
Latitude 
{ 
get 
; 
set  #
;# $
}% &
[ 
JsonPropertyName 
( 
$str +
)+ ,
], -
public 
double	 
	Longitude 
{ 
get 
;  
set! $
;$ %
}& '
[ 
JsonPropertyName 
( 
$str ,
), -
]- .
public 
required	 
string 
AddressString &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
[ 
JsonPropertyName 
( 
$str &
)& '
]' (
public 
required	 
string 
Locality !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 
JsonPropertyName 
( 
$str )
)) *
]* +
public 
required	 
string 

PostalCode #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} ˛
G/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/HostDto.cs
public 
class 
HostDto 
{ 
public 
required	 
string 
HostId 
{  !
get" %
;% &
set' *
;* +
}, -
[ 
JsonPropertyName 
( 
$str 
) 
] 
public 
required	 
string 
HostName !
{" #
get$ '
;' (
set) ,
;, -
}. /
[

 
JsonPropertyName

 
(

 
$str

 ,
)

, -
]

- .
public 
required	 
string 
HostAddress $
{% &
get' *
;* +
set, /
;/ 0
}1 2
[ 
JsonPropertyName 
( 
$str &
)& '
]' (
public 
required	 
string 
HostLocality %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
[ 
JsonPropertyName 
( 
$str )
)) *
]* +
public 
required	 
string 
HostPostalCode '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
[ 
JsonPropertyName 
( 
$str ,
), -
]- .
public 
required	 
string 
OperatorPhone &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
[ 
JsonPropertyName 
( 
$str .
). /
]/ 0
public 
required	 
string 
OperatorWebsite (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
required	 
string 

LocationId #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
LocationDto	 
? 
Location 
{  
get! $
;$ %
set& )
;) *
}+ ,
} Ñ
Q/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/CombinedBicingDto.cs
public 
class 
CombinedBicingDto 
{ 
public 
BicingStationDto	 
StationInfo %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
StateBicingDto	 
RealTimeStatus &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
} ó
R/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/ChargingStationDto.cs
public 
class 
ChargingStationDto 
{ 
public 
string	 
	StationId 
{ 
get 
;  
set! $
;$ %
}& '
public 
string	 
StationLabel 
{ 
get "
;" #
set$ '
;' (
}) *
public 
double	 
StationLatitude 
{  !
get" %
;% &
set' *
;* +
}, -
public		 
double			 
StationLongitude		  
{		! "
get		# &
;		& '
set		( +
;		+ ,
}		- .
public 
string	 
AddressString 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string	 
Locality 
{ 
get 
; 
set  #
;# $
}% &
public 
string	 

PostalCode 
{ 
get  
;  !
set" %
;% &
}' (
public 
double	 
LocationLatitude  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
double	 
LocationLongitude !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string	 
HostId 
{ 
get 
; 
set !
;! "
}# $
public 
string	 
HostName 
{ 
get 
; 
set  #
;# $
}% &
public 
string	 
	HostPhone 
{ 
get 
;  
set! $
;$ %
}& '
public 
string	 
HostWebsite 
{ 
get !
;! "
set# &
;& '
}( )
public 
List	 
< 
PortDto 
> 
Ports 
{ 
get "
;" #
set$ '
;' (
}) *
} Ô
F/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/BusDto.cs
	namespace 	
Dto
 
; 
public 
class 
BusDto 
{ 
public 
int	 
ParadaId 
{ 
get 
; 
set  
;  !
}" #
public 
int	 

CodiParada 
{ 
get 
; 
set "
;" #
}$ %
public 
string	 
Name 
{ 
get 
; 
set 
;  
}! "
public 
string	 
Description 
{ 
get !
;! "
set# &
;& '
}( )
public		 
int			 
IntersectionId		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
public

 
string

	 
IntersectionName

  
{

! "
get

# &
;

& '
set

( +
;

+ ,
}

- .
public 
string	 
ParadaTypeName 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string	 "
ParadaTypeTipification &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string	 
Adress 
{ 
get 
; 
set !
;! "
}# $
public 
string	 

LocationId 
{ 
get  
;  !
set" %
;% &
}' (
public 
string	 
LocationName 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string	 

DistrictId 
{ 
get  
;  !
set" %
;% &
}' (
public 
string	 
DistrictName 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string	 
Date 
{ 
get 
; 
set 
;  
}! "
public 
string	 

StreetName 
{ 
get  
;  !
set" %
;% &
}' (
public 
string	 
ParadaPoints 
{ 
get "
;" #
set$ '
;' (
}) *
public 
double	 
Latitude 
{ 
get 
; 
set  #
;# $
}% &
public 
double	 
	Longitude 
{ 
get 
;  
set! $
;$ %
}& '
} ñ
P/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Dto/BicingStationDto.cs
	namespace 	
Dto
 
; 
public 
class 
BicingStationDto 
{ 
[		 
JsonPropertyName		 
(		 
$str		  
)		  !
]		! "
public

 
required

	 
int

 
BicingId

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
[ 
JsonPropertyName 
( 
$str 
) 
] 
public 
required	 
string 

BicingName #
{$ %
get& )
;) *
set+ .
;. /
}0 1
[ 
JsonPropertyName 
( 
$str 
) 
] 
public 
required	 
double 
Latitude !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 
JsonPropertyName 
( 
$str 
) 
] 
public 
required	 
double 
	Longitude "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 
JsonPropertyName 
( 
$str 
) 
]  
public 
required	 
double 
Altitude !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 
JsonPropertyName 
( 
$str 
) 
] 
public 
required	 
string 
Address  
{! "
get# &
;& '
set( +
;+ ,
}- .
[ 
JsonPropertyName 
( 
$str "
)" #
]# $
public 
required	 
string 
CrossStreet $
{% &
get' *
;* +
set, /
;/ 0
}1 2
[ 
JsonPropertyName 
( 
$str 
)  
]  !
public 
required	 
string 
PostCode !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 
JsonPropertyName 
( 
$str 
) 
]  
public 
required	 
int 
Capacity 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 
JsonPropertyName 
( 
$str )
)) *
]* +
public 
required	 
bool 
IsChargingStation (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
} Á?
[/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Controllers/UbicationController.cs
	namespace		 	
	plantilla		
 
.		 
Web		 
.		 
src		 
.		 
Controllers		 '
;		' (
[ 
ApiController 
] 
[ 
Route 
( 
$str 
) 
] 
[ 
	Authorize 

]
 
public 
class 
UbicationController  
(  !
IUbicationService! 2
ubicationService3 C
)C D
:E F
ControllerBaseG U
{ 
private 	
readonly
 
IUbicationService $
_ubicationService% 6
=7 8
ubicationService9 I
;I J
[ 
HttpGet 

(
 
$str 
)  
]  !
public 
async	 
Task 
< 
IActionResult !
>! "!
GetAllSavedUbications# 8
(8 9
[9 :
	FromQuery: C
]C D
stringE K
	userEmailL U
)U V
{ 
if 
( 
string 
. 
IsNullOrEmpty 
( 
	userEmail &
)& '
)' (
{ 
return 

BadRequest 
( 
$str /
)/ 0
;0 1
} 
var 
savedUbications 
= 
await 
_ubicationService  1
.1 2&
GetUbicationsByUserIdAsync2 L
(L M
	userEmailM V
)V W
.W X
ConfigureAwaitX f
(f g
falseg l
)l m
;m n
if 
( 
savedUbications 
== 
null 
||  "
savedUbications# 2
.2 3
Count3 8
==9 ;
$num< =
)= >
{ 
return   
NotFound   
(   
$str   @
)  @ A
;  A B
}!! 
return## 

Ok## 
(## 
savedUbications## 
)## 
;## 
}$$ 
[&& 
HttpPost&& 
(&& 
$str&& 
)&& 
]&& 
public'' 
async''	 
Task'' 
<'' 
IActionResult'' !
>''! "
SaveUbication''# 0
(''0 1
[''1 2
FromBody''2 :
]'': ;
SavedUbicationDto''< M
savedUbication''N \
)''\ ]
{(( 
if)) 
()) 
savedUbication)) 
==)) 
null)) 
))) 
{** 
return++ 

BadRequest++ 
(++ 
$str++ ;
)++; <
;++< =
},, 
var-- 
result-- 
=-- 
await-- 
_ubicationService-- (
.--( )
SaveUbicationAsync--) ;
(--; <
savedUbication--< J
)--J K
.--K L
ConfigureAwait--L Z
(--Z [
false--[ `
)--` a
;--a b
if.. 
(.. 
result.. 
==.. 
false.. 
).. 
{// 
return00 

BadRequest00 
(00 
$str00 3
)003 4
;004 5
}11 
return22 

Ok22 
(22 
$str22 -
)22- .
;22. /
}33 
[55 

HttpDelete55 
(55 
$str55 
)55  
]55  !
public66 
async66	 
Task66 
<66 
IActionResult66 !
>66! "
DeleteUbication66# 2
(662 3
[663 4
FromBody664 <
]66< =
UbicationInfoDto66> N
ubicationDeleteDto66O a
)66a b
{77 
if88 
(88 
ubicationDeleteDto88 
==88 
null88 "
)88" #
{99 
return:: 

BadRequest:: 
(:: 
$str:: 5
)::5 6
;::6 7
};; 
var<< 
done<< 
=<< 
await<< 
_ubicationService<< &
.<<& '
DeleteUbication<<' 6
(<<6 7
ubicationDeleteDto<<7 I
)<<I J
.<<J K
ConfigureAwait<<K Y
(<<Y Z
false<<Z _
)<<_ `
;<<` a
if== 
(== 
done== 
==== 
false== 
)== 
{>> 
return?? 

BadRequest?? 
(?? 
$str?? 5
)??5 6
;??6 7
}@@ 
returnAA 

OkAA 
(AA 
$strAA /
)AA/ 0
;AA0 1
}CC 
[DD 
HttpPostDD 
(DD 
$strDD 
)DD 
]DD 
publicEE 
asyncEE	 
TaskEE 
<EE 
IActionResultEE !
>EE! "
ValorateEE# +
(EE+ ,
[EE, -
FromBodyEE- 5
]EE5 6
UbicationInfoDtoEE7 G
ubicationInfoDtoEEH X
)EEX Y
{FF 
ifGG 
(GG 
ubicationInfoDtoGG 
==GG 
nullGG  
)GG  !
{HH 
returnII 

BadRequestII 
(II 
$strII 5
)II5 6
;II6 7
}JJ 
ifKK 
(KK 
ubicationInfoDtoKK 
.KK 

ValorationKK #
==KK$ &
nullKK' +
)KK+ ,
{LL 
returnMM 

BadRequestMM 
(MM 
$strMM 1
)MM1 2
;MM2 3
}NN 
ifOO 
(OO 
ubicationInfoDtoOO 
.OO 

ValorationOO #
<OO$ %
$numOO& '
||OO( *
ubicationInfoDtoOO+ ;
.OO; <

ValorationOO< F
>OOG H
$numOOI J
)OOJ K
{PP 
returnQQ 

BadRequestQQ 
(QQ 
$strQQ =
)QQ= >
;QQ> ?
}RR 
varSS 
resultSS 
=SS 
awaitSS 
_ubicationServiceSS (
.SS( )
UpdateUbicationSS) 8
(SS8 9
ubicationInfoDtoSS9 I
)SSI J
.SSJ K
ConfigureAwaitSSK Y
(SSY Z
falseSSZ _
)SS_ `
;SS` a
ifTT 
(TT 
resultTT 
==TT 
falseTT 
)TT 
{UU 
returnVV 

BadRequestVV 
(VV 
$strVV 7
)VV7 8
;VV8 9
}WW 
returnXX 

OkXX 
(XX 
$strXX 1
)XX1 2
;XX2 3
}YY 
[ZZ 
HttpGetZZ 

(ZZ
 
$strZZ 
)ZZ 
]ZZ 
public[[ 
async[[	 
Task[[ 
<[[ 
IActionResult[[ !
>[[! "
GetUbicationDetails[[# 6
([[6 7
[\\ 
	FromQuery\\ 
]\\ 
int\\ 
ubicationId\\ !
,\\! "
[]] 
	FromQuery]] 
]]] 
string]] 
stationType]] $
)]]$ %
{^^ 
var__ 
result__ 
=__ 
await__ 
_ubicationService__ (
.__( )
GetUbicationDetails__) <
(__< =
ubicationId__= H
,__H I
stationType__J U
)__U V
.__V W
ConfigureAwait__W e
(__e f
false__f k
)__k l
;__l m
if`` 
(`` 
result`` 
==`` 
null`` 
)`` 
{aa 
returnbb 
NotFoundbb 
(bb 
$strbb <
)bb< =
;bb= >
}cc 
returndd 

Okdd 
(dd 
resultdd 
)dd 
;dd 
}ee 
}ff Ó9
V/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Controllers/UserController.cs
	namespace		 	
src		
 
.		 
Controllers		 
;		 
[ 
Route 
( 
$str 
) 
] 
[ 
ApiController 
] 
[ 
	Authorize 

]
 
public 
class 
UserController 
: 
ControllerBase ,
{ 
private 	
readonly
 
IUserService 
_userService  ,
;, -
public 
UserController	 
( 
IUserService $
userService% 0
)0 1
{ 
_userService 
= 
userService 
; 
} 
[ 
HttpGet 

(
 
$str 
) 
] 
public 
IActionResult	 
GetUserstest #
(# $
)$ %
{ 
var 
users 
= 
new 
[ 
] 
{ 
new 
{ 
Id 
= 
$num 
, 
Name 
=  
$str! '
,' (
Email) .
=/ 0
$str1 C
,C D
PasswordHashE Q
=R S
$strT a
,a b
Idiomac i
=j k
$strl p
}q r
,r s
new 
{ 
Id 
= 
$num 
, 
Name 
=  
$str! &
,& '
Email( -
=. /
$str0 A
,A B
PasswordHashC O
=P Q
$strR _
,_ `
Idiomaa g
=h i
$strj n
}o p
} 	
;	 

return   

Ok   
(   
users   
)   
;   
}!! 
[$$ 
HttpPost$$ 
($$ 
$str$$ 
)$$ 
]$$ 
[%% 
AllowAnonymous%% 
]%% 
public&& 
IActionResult&&	 

CreateUser&& !
(&&! "
[&&" #
FromBody&&# +
]&&+ ,

UserCreate&&- 7
user&&8 <
)&&< =
{'' 
if(( 
((( 
user(( 
==(( 
null(( 
)(( 
{)) 
return** 

BadRequest** 
(** 
$str** &
)**& '
;**' (
}++ 
try-- 
{.. 
var// 	
	isCreated//
 
=// 
_userService// "
.//" #

CreateUser//# -
(//- .
user//. 2
)//2 3
;//3 4
if11 
(11	 

	isCreated11
 
)11 
{22 
return33 
Ok33 
(33 
$str33 -
)33- .
;33. /
}44 
return55 

BadRequest55 
(55 
$str55 
)55  
;55  !
}66 
catch77 	
(77
 
	Exception77 
ex77 
)77 
{88 
return99 

StatusCode99 
(99 
$num99 
,99 
$"99 
$str99 5
{995 6
ex996 8
.998 9
Message999 @
}99@ A
"99A B
)99B C
;99C D
}:: 
};; 
[== 

HttpDelete== 
(== 
$str== 
)== 
]== 
public>> 
async>>	 
Task>> 
<>> 
IActionResult>> !
>>>! "

DeleteUser>># -
(>>- .
[>>. /
FromBody>>/ 7
]>>7 8
UserCredentials>>9 H
user>>I M
)>>M N
{?? 
if@@ 
(@@ 
user@@ 
==@@ 
null@@ 
)@@ 
{AA 
returnBB 

BadRequestBB 
(BB 
$strBB &
)BB& '
;BB' (
}CC 
tryEE 
{FF 
varGG 	
	isDeletedGG
 
=GG 
awaitGG 
_userServiceGG (
.GG( )

DeleteUserGG) 3
(GG3 4
userGG4 8
)GG8 9
.GG9 :
ConfigureAwaitGG: H
(GGH I
falseGGI N
)GGN O
;GGO P
ifII 
(II	 

	isDeletedII
 
)II 
{JJ 
returnKK 
OkKK 
(KK 
$strKK -
)KK- .
;KK. /
}LL 
returnMM 

BadRequestMM 
(MM 
$strMM /
)MM/ 0
;MM0 1
}NN 
catchOO 	
(OO
 
	ExceptionOO 
exOO 
)OO 
{PP 
returnQQ 

StatusCodeQQ 
(QQ 
$numQQ 
,QQ 
$"QQ 
$strQQ 5
{QQ5 6
exQQ6 8
.QQ8 9
MessageQQ9 @
}QQ@ A
"QQA B
)QQB C
;QQC D
}RR 
}SS 
[UU 
HttpGetUU 

(UU
 
$strUU 
)UU 
]UU 
publicVV 
IActionResultVV	 
GetUsersVV 
(VV  
)VV  !
{WW 
varXX 
usersXX 
=XX 
_userServiceXX 
.XX 
GetAllUsersXX (
(XX( )
)XX) *
;XX* +
returnYY 

OkYY 
(YY 
usersYY 
)YY 
;YY 
}ZZ 
[\\ 
HttpPost\\ 
(\\ 
$str\\ 
)\\ 
]\\ 
public]] 
async]]	 
Task]] 
<]] 
IActionResult]] !
>]]! "

ModifyUser]]# -
(]]- .
[]]. /
FromBody]]/ 7
]]]7 8
UserDto]]9 @
user]]A E
)]]E F
{^^ 
if__ 
(__ 
user__ 
==__ 
null__ 
)__ 
{`` 
returnaa 

BadRequestaa 
(aa 
$straa &
)aa& '
;aa' (
}bb 
trydd 
{ee 
varff 	

isModifiedff
 
=ff 
awaitff 
_userServiceff )
.ff) *

ModifyUserff* 4
(ff4 5
userff5 9
)ff9 :
.ff: ;
ConfigureAwaitff; I
(ffI J
falseffJ O
)ffO P
;ffP Q
ifhh 
(hh	 


isModifiedhh
 
)hh 
{ii 
returnjj 
Okjj 
(jj 
$strjj .
)jj. /
;jj/ 0
}kk 
returnll 

BadRequestll 
(ll 
$strll /
)ll/ 0
;ll0 1
}mm 
catchnn 	
(nn
 
	Exceptionnn 
exnn 
)nn 
{oo 
returnpp 

StatusCodepp 
(pp 
$numpp 
,pp 
$"pp 
$strpp 5
{pp5 6
expp6 8
.pp8 9
Messagepp9 @
}pp@ A
"ppA B
)ppB C
;ppC D
}qq 
}rr 
}ss ˚
U/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Controllers/TmbController.cs
	namespace 	
Controllers
 
; 
[		 
Route		 
(		 
$str		 
)		 
]		 
[

 
ApiController

 
]

 
[ 
	Authorize 

]
 
public 
class 
TmbController 
: 
ControllerBase +
{ 
private 	
readonly
 
ITmbService 
_tmbService *
;* +
public 
TmbController	 
( 
ITmbService "

tmbService# -
)- .
{ 
_tmbService 
= 

tmbService 
; 
} 
[ 
HttpGet 

(
 
$str 
) 
] 
public 
async	 
Task 
< 
IActionResult !
>! "
GetAllMetros# /
(/ 0
)0 1
{ 
var 
metros 
= 
await 
_tmbService "
." #
GetAllMetrosAsync# 4
(4 5
)5 6
.6 7
ConfigureAwait7 E
(E F
falseF K
)K L
;L M
return 

Ok 
( 
metros 
) 
; 
} 
[ 
HttpGet 

(
 
$str 
) 
] 
public 
async	 
Task 
< 
IActionResult !
>! "
	GetAllBus# ,
(, -
)- .
{ 
var   
bus   
=   
await   
_tmbService   
.    
GetAllBusAsync    .
(  . /
)  / 0
.  0 1
ConfigureAwait  1 ?
(  ? @
false  @ E
)  E F
;  F G
return!! 

Ok!! 
(!! 
bus!! 
)!! 
;!! 
}"" 
}## ÷
W/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Controllers/RouteController.cs
[ 
ApiController 
] 
[ 
Route 
( 
$str 
) 
] 
[ 
	Authorize 

]
 
public 
class 
RutasController 
: 
ControllerBase -
{ 
private 	
readonly
 
IRouteService  
_service! )
;) *
private 	
readonly
 
IRouteRepository #
_repo$ )
;) *
public 
RutasController	 
( 
IRouteService &
service' .
,. /
IRouteRepository0 @
repoA E
)E F
{ 
_service 
= 
service 
; 
_repo 	
=
 
repo 
; 
} 
[ 
HttpPost 
( 
$str 
) 
] 
public 
async	 
Task 
< 
IActionResult !
>! "
CalcularRuta# /
(/ 0
[0 1
FromBody1 9
]9 :
RouteRequestDto; J
dtoK N
)N O
{ 
Guid 
	usuarioId	 
= 
Guid 
. 
Parse 
(  
$str  F
)F G
;G H
var 
	resultado 
= 
await 
_service "
." #
CalcularRutaAsync# 4
(4 5
dto5 8
,8 9
	usuarioId: C
)C D
.D E
ConfigureAwaitE S
(S T
falseT Y
)Y Z
;Z [
return 

Ok 
( 
	resultado 
) 
; 
} 
}   ˆ
]/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Controllers/PublicRouteController.cs
[ 
ApiController 
] 
[		 
Route		 
(		 
$str		 
)		 
]		 
public

 
class

 !
PublicRutasController

 "
:

# $
ControllerBase

% 3
{ 
private 	
readonly
 
IRouteService  
_service! )
;) *
private 	
readonly
 
IConfiguration !
_config" )
;) *
public !
PublicRutasController	 
( 
IRouteService ,
service- 4
,4 5
IConfiguration6 D
configE K
)K L
{ 
_service 
= 
service 
; 
_config 
= 
config 
; 
} 
[ 
HttpPost 
( 
$str 
) 
] 
public 
async	 
Task 
< 
IActionResult !
>! "
CalcularRutaPublica# 6
(6 7
[7 8
FromBody8 @
]@ A
RouteRequestDtoB Q
dtoR U
,U V
[W X

FromHeaderX b
(b c
Namec g
=h i
$strj u
)u v
]v w
stringx ~
apiKey	 Ö
)
Ö Ü
{ 
var 
expectedKey 
= 
_config 
[ 
$str 6
]6 7
;7 8
if 
( 
apiKey 
!= 
expectedKey 
) 
{ 
return 
Unauthorized 
( 
$str +
)+ ,
;, -
} 
var 
	resultado 
= 
await 
_service "
." #
CalcularRutaAsync# 4
(4 5
dto5 8
,8 9
Guid: >
.> ?
Empty? D
)D E
.E F
ConfigureAwaitF T
(T U
falseU Z
)Z [
;[ \
return 

Ok 
( 
	resultado 
) 
; 
}   
}!! Á
b/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Controllers/ChargingStationsController.cs
	namespace 	
Controllers
 
; 
[

 
ApiController

 
]

 
[ 
Route 
( 
$str 
) 
] 
[ 
	Authorize 

]
 
public 
class &
ChargingStationsController '
:( )
ControllerBase* 8
{ 
private 	
readonly
 $
IChargingStationsService +#
_chargingStationService, C
;C D
public &
ChargingStationsController	 #
(# $$
IChargingStationsService$ <
dadesObertesService= P
)P Q
{ #
_chargingStationService 
= 
dadesObertesService 1
;1 2
} 
[ 
HttpGet 

(
 
$str 
) 
] 
public 
async	 
Task 
< 
IActionResult !
>! "
GetAllStations# 1
(1 2
)2 3
{ 
var 
stations 
= 
await #
_chargingStationService 0
.0 1'
GetAllChargingStationsAsync1 L
(L M
)M N
.N O
ConfigureAwaitO ]
(] ^
false^ c
)c d
;d e
return 

Ok 
( 
stations 
) 
; 
} 
} ∆
_/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Controllers/BicingStationController.cs
	namespace

 	
Controllers


 
;

 
[ 
ApiController 
] 
[ 
Route 
( 
$str 
) 
] 
[ 
	Authorize 

]
 
public 
class #
BicingStationController $
:% &
ControllerBase' 5
{ 
private 	
readonly
 !
IBicingStationService (!
_bicingStationService) >
;> ?
private 	
readonly
 
IStateBicingService &
_stateBicingService' :
;: ;
private 	
readonly
 
IMapper 
_mapper "
;" #
public #
BicingStationController	  
(  !!
IBicingStationService! 6 
bicingStationService7 K
,K L
IStateBicingServiceM `
stateBicingServicea s
,s t
IMapperu |
mapper	} É
)
É Ñ
{ !
_bicingStationService 
=  
bicingStationService 0
;0 1
_mapper 
= 
mapper 
; 
_stateBicingService 
= 
stateBicingService ,
;, -
} 
[ 
HttpGet 

(
 
$str 
) 
] 
public 
async	 
Task 
< 
IActionResult !
>! "
GetAllStations# 1
(1 2
)2 3
{   
var!! 
stations!! 
=!! 
await!! !
_bicingStationService!! .
.!!. /%
GetAllBicingStationsAsync!!/ H
(!!H I
)!!I J
.!!J K
ConfigureAwait!!K Y
(!!Y Z
false!!Z _
)!!_ `
;!!` a
var"" 
states"" 
="" 
await"" 
_stateBicingService"" *
.""* +*
GetAllStateBicingStationsAsync""+ I
(""I J
)""J K
.""K L
ConfigureAwait""L Z
(""Z [
false""[ `
)""` a
;""a b
var$$ 
combinedData$$ 
=$$ 
stations$$ 
.$$  
Join$$  $
($$$ %
states%% 
,%% 
station&& 
=>&& 
station&& 
.&& 
BicingId&& #
,&&# $
state'' 
=>'' 
state'' 
.'' 
BicingId'' 
,''  
((( 	
station((	 
,(( 
state(( 
)(( 
=>(( 
new(( 
CombinedBicingDto((  1
{)) 	
StationInfo**
 
=** 
_mapper** 
.**  
Map**  #
<**# $
BicingStationDto**$ 4
>**4 5
(**5 6
station**6 =
)**= >
,**> ?
RealTimeStatus++
 
=++ 
_mapper++ "
.++" #
Map++# &
<++& '
StateBicingDto++' 5
>++5 6
(++6 7
state++7 <
)++< =
},, 	
)-- 
.-- 
ToList-- 
(-- 
)-- 
;-- 
return// 

Ok// 
(// 
combinedData// 
)// 
;// 
}00 
}11 ∫/
`/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Controllers/AuthoritzationController.cs
	namespace

 	
Controllers


 
;

 
[ 
ApiController 
] 
[ 
Route 
( 
$str 
) 
] 
public 
class #
AuthorizationController $
:% &
ControllerBase' 5
{ 
private 	
readonly
 
IUserService 
_userService  ,
;, -
private 	
readonly
 !
IAuthenticationHelper (!
_authenticationHelper) >
;> ?
public #
AuthorizationController	  
(  !
IUserService! -
userService. 9
,9 :!
IAuthenticationHelper; P 
authenticationHelperQ e
)e f
{ 
_userService 
= 
userService 
; !
_authenticationHelper 
=  
authenticationHelper 0
;0 1
} 
[ 
HttpPost 
( 
$str 
) 
] 
public 
async	 
Task 
< 
IActionResult !
>! "
Login# (
(( )
[) *
FromBody* 2
]2 3
UserCredentials4 C
userCredentialsD S
)S T
{ 
if 
( 
userCredentials 
== 
null 
)  
{ 
return 

BadRequest 
( 
$str &
)& '
;' (
}   
try"" 
{## 
var$$ 	
user$$
 
=$$ 
await$$ 
_userService$$ #
.$$# $
Authenticate$$$ 0
($$0 1
userCredentials$$1 @
)$$@ A
.$$A B
ConfigureAwait$$B P
($$P Q
false$$Q V
)$$V W
;$$W X
if%% 
(%%	 

user%%
 
==%% 
null%% 
)%% 
{&& 
return'' 
Unauthorized'' 
('' 
$str'' 1
)''1 2
;''2 3
}(( 
var)) 	
())
 
claimsIdentity)) 
,)) $
authenticationProperties)) 3
)))3 4
=))5 6!
_authenticationHelper))7 L
.))L M 
AuthenticationClaims))M a
())a b
user))b f
)))f g
;))g h
await** 
HttpContext** 
.** 
SignInAsync** #
(**# $(
CookieAuthenticationDefaults**$ @
.**@ A 
AuthenticationScheme**A U
,**U V
new**W Z
ClaimsPrincipal**[ j
(**j k
claimsIdentity**k y
)**y z
,**z {%
authenticationProperties	**| î
)
**î ï
.
**ï ñ
ConfigureAwait
**ñ §
(
**§ •
false
**• ™
)
**™ ´
;
**´ ¨
return++ 
Ok++ 
(++ 
$str++ 
)++ 
;++ 
},, 
catch-- 	
(--
 
	Exception-- 
ex-- 
)-- 
{.. 
return// 

StatusCode// 
(// 
$num// 
,// 
$"// 
$str// 5
{//5 6
ex//6 8
.//8 9
Message//9 @
}//@ A
"//A B
)//B C
;//C D
}00 
}11 
[44 
HttpPost44 
(44 
$str44 
)44 
]44 
public55 
async55	 
Task55 
<55 
IActionResult55 !
>55! "
GoogleLogin55# .
(55. /
[55/ 0
FromBody550 8
]558 9
LoginGoogleDto55: H
dto55I L
)55L M
{66 
if77 
(77 
string77 
.77 
IsNullOrEmpty77 
(77 
dto77  
.77  !
Email77! &
)77& '
||77( *
string77+ 1
.771 2
IsNullOrEmpty772 ?
(77? @
dto77@ C
.77C D
Username77D L
)77L M
)77M N
return88 

BadRequest88 
(88 
$str88 &
)88& '
;88' (
try:: 
{;; 
var<< 	
user<<
 
=<< 
await<< 
_userService<< #
.<<# $ 
LoginWithGoogleAsync<<$ 8
(<<8 9
dto<<9 <
)<<< =
.<<= >
ConfigureAwait<<> L
(<<L M
false<<M R
)<<R S
;<<S T
var== 	
(==
 
claimsIdentity== 
,== $
authenticationProperties== 3
)==3 4
===5 6!
_authenticationHelper==7 L
.==L M 
AuthenticationClaims==M a
(==a b
user==b f
)==f g
;==g h
await>> 
HttpContext>> 
.>> 
SignInAsync>> #
(>># $(
CookieAuthenticationDefaults>>$ @
.>>@ A 
AuthenticationScheme>>A U
,>>U V
new>>W Z
ClaimsPrincipal>>[ j
(>>j k
claimsIdentity>>k y
)>>y z
,>>z {%
authenticationProperties	>>| î
)
>>î ï
.
>>ï ñ
ConfigureAwait
>>ñ §
(
>>§ •
false
>>• ™
)
>>™ ´
;
>>´ ¨
return?? 
Ok?? 
(?? 
user?? 
)?? 
;?? 
}@@ 
catchAA 	
(AA
 
	ExceptionAA 
exAA 
)AA 
{BB 
returnCC 

StatusCodeCC 
(CC 
$numCC 
,CC 
$"CC 
$strCC -
{CC- .
exCC. 0
.CC0 1
MessageCC1 8
}CC8 9
"CC9 :
)CC: ;
;CC; <
}DD 
}EE 
}FF ä
\/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Constants/UbicationTypeConstants.cs
	namespace 	
	Constants
 
; 
public 
static 
class "
UbicationTypeConstants *
{ 
public 
const	 
string 
BICING 
= 
$str '
;' (
public 
const	 
string 
BUS 
= 
$str !
;! "
public 
const	 
string 
METRO 
= 
$str %
;% &
public 
const	 
string 
CHARGING 
=  
$str! +
;+ ,
}		 ˜
S/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/src/Constants/ApiClaimTypes.cs
	namespace 	
	Constants
 
; 
public 
static 
class 
ApiClaimTypes !
{ 
public 
const	 
string 
UserId 
= 
$str '
;' (
public 
const	 
string 
Name 
= 
$str #
;# $
public 
const	 
string 
Email 
= 
$str %
;% &
public 
const	 
string 
Idioma 
= 
$str '
;' (
}		 ©C
?/home/daniel/Escritorio/Uni/PES/repos/E-MoveBCN-back/Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder 
. 
Services 
. 
AddHttpClient 
( 
)  
;  !
builder 
. 
Services 
. 
AddControllers 
(  
)  !
;! "
builder 
. 
Services 
. 
AddServices 
( 
) 
; 
builder   
.   
Services   
.   
AddDbContext   
<   
ApiDbContext   *
>  * +
(  + ,
options  , 3
=>  4 6
options!! 
.!! 
	UseNpgsql!! 
(!! 
builder!! 
.!! 
Configuration!! +
.!!+ ,
GetConnectionString!!, ?
(!!? @
$str!!@ S
)!!S T
)!!T U
)!!U V
;!!V W
builder$$ 
.$$ 
Services$$ 
.$$ 
AddAutoMapper$$ 
($$ 
typeof$$ %
($$% &
MapperProfiles$$& 4
)$$4 5
)$$5 6
;$$6 7
builder'' 
.'' 
Services'' 
.'' 
AddAuthentication'' "
(''" #(
CookieAuthenticationDefaults''# ?
.''? @ 
AuthenticationScheme''@ T
)''T U
.(( 
	AddCookie(( 
((( 
options(( 
=>(( 
{)) 
options** 
.** 
Cookie** 
.** 
SameSite** 
=** 
SameSiteMode**  ,
.**, -
Lax**- 0
;**0 1
options++ 
.++ 
Cookie++ 
.++ 
SecurePolicy++ !
=++" #
CookieSecurePolicy++$ 6
.++6 7
Always++7 =
;++= >
options,, 
.,, 
SlidingExpiration,, 
=,,  !
true,," &
;,,& '
options-- 
.-- 
Cookie-- 
.-- 
HttpOnly-- 
=-- 
true--  $
;--$ %
options.. 
... 
Cookie.. 
... 
IsEssential..  
=..! "
true..# '
;..' (
options// 
.// 
Events// 
=// 
new// &
CookieAuthenticationEvents// 5
{00 
OnRedirectToLogin11 
=11 
context11 #
=>11$ &
{22 	
context33
 
.33 
Response33 
.33 

StatusCode33 %
=33& '
StatusCodes33( 3
.333 4!
Status401Unauthorized334 I
;33I J
return44
 
Task44 
.44 
CompletedTask44 #
;44# $
}55 	
,55	 
$
OnRedirectToAccessDenied66  
=66! "
context66# *
=>66+ -
{77 	
context88
 
.88 
Response88 
.88 

StatusCode88 %
=88& '
StatusCodes88( 3
.883 4
Status403Forbidden884 F
;88F G
return99
 
Task99 
.99 
CompletedTask99 #
;99# $
}:: 	
};; 
;;; 
}<< 
)<< 
;<< 
builderAA 
.AA 
ServicesAA 
.AA 
AddCorsAA 
(AA 
optionsAA  
=>AA! #
{BB 
optionsCC 	
.CC	 

	AddPolicyCC
 
(CC 
$strCC 
,CC 
policyDD 
=>DD 
policyDD 
.DD 
AllowAnyOriginDD %
(DD% &
)DD& '
.EE 
AllowAnyMethodEE %
(EE% &
)EE& '
.FF 
AllowAnyHeaderFF %
(FF% &
)FF& '
)FF' (
;FF( )
}GG 
)GG 
;GG 
builderKK 
.KK 
ServicesKK 
.KK 
AddSwaggerGenKK 
(KK 
cKK  
=>KK! #
{LL 
cMM 
.MM 

SwaggerDocMM 
(MM 
$strMM 
,MM 
newMM 
OpenApiInfoMM $
{NN 
TitleOO 	
=OO
 
$strOO 
,OO 
VersionPP 
=PP 
$strPP 
,PP 
DescriptionQQ 
=QQ 
$strQQ :
,QQ: ;
ContactRR 
=RR 
newRR 
OpenApiContactRR  
{SS 
NameTT 

=TT 
$strTT 
,TT 
EmailUU 
=UU 
$strUU #
,UU# $
UrlVV 	
=VV
 
newVV 
UriVV 
(VV 
$strVV '
)VV' (
}WW 
}XX 
)XX 
;XX 
c[[ 
.[[ !
AddSecurityDefinition[[ 
([[ 
$str[[ "
,[[" #
new[[$ '!
OpenApiSecurityScheme[[( =
{\\ 
Name]] 
=]]	 

$str]] 
,]] 
Type^^ 
=^^	 

SecuritySchemeType^^ 
.^^ 
Http^^ "
,^^" #
Scheme__ 

=__ 
$str__ 
,__ 
BearerFormat`` 
=`` 
$str`` 
,`` 
Inaa 
=aa 
ParameterLocationaa	 
.aa 
Headeraa !
,aa! "
Descriptionbb 
=bb 
$strbb [
}cc 
)cc 
;cc 
cee 
.ee "
AddSecurityRequirementee 
(ee 
newee &
OpenApiSecurityRequirementee 9
{ff 
{gg 	
newhh !
OpenApiSecuritySchemehh %
{ii 
	Referencejj 
=jj 
newjj 
OpenApiReferencejj  0
{kk 
Typell 
=ll 
ReferenceTypell (
.ll( )
SecuritySchemell) 7
,ll7 8
Idmm 
=mm 
$strmm !
}nn 
}oo 
,oo 
newpp 
Listpp 
<pp 
stringpp 
>pp 
(pp 
)pp 
}qq 	
}rr 
)rr 
;rr 
}ss 
)ss 
;ss 
varuu 
appuu 
=uu 	
builderuu
 
.uu 
Builduu 
(uu 
)uu 
;uu 
varvv 
portvv 
=vv	 

Environmentvv 
.vv "
GetEnvironmentVariablevv -
(vv- .
$strvv. 4
)vv4 5
??vv6 8
$strvv9 ?
;vv? @
appww 
.ww 
Urlsww 
.ww 	
Clearww	 
(ww 
)ww 
;ww 
appxx 
.xx 
Urlsxx 
.xx 	
Addxx	 
(xx 
$"xx 
$strxx 
{xx 
portxx 
}xx 
"xx 
)xx  
;xx  !
Consoleyy 
.yy 
	WriteLineyy 
(yy 
$"yy 
$stryy +
{yy+ ,
portyy, 0
}yy0 1
"yy1 2
)yy2 3
;yy3 4
app{{ 
.{{ 

UseSwagger{{ 
({{ 
){{ 
;{{ 
app|| 
.|| 
UseSwaggerUI|| 
(|| 
c|| 
=>|| 
{}} 
c~~ 
.~~ 
SwaggerEndpoint~~ 
(~~ 
$str~~ .
,~~. /
$str~~0 ;
)~~; <
;~~< =
c 
. 
RoutePrefix 
= 
string 
. 
Empty 
; 
}ÄÄ 
)
ÄÄ 
;
ÄÄ 
appÅÅ 
.
ÅÅ 
UseCors
ÅÅ 
(
ÅÅ 
$str
ÅÅ 
)
ÅÅ 
;
ÅÅ 
appÇÇ 
.
ÇÇ 
UseAuthentication
ÇÇ 
(
ÇÇ 
)
ÇÇ 
;
ÇÇ 
appÉÉ 
.
ÉÉ 
UseAuthorization
ÉÉ 
(
ÉÉ 
)
ÉÉ 
;
ÉÉ 
appÑÑ 
.
ÑÑ 
MapControllers
ÑÑ 
(
ÑÑ 
)
ÑÑ 
;
ÑÑ 
appÜÜ 
.
ÜÜ 
Run
ÜÜ 
(
ÜÜ 
)
ÜÜ 	
;
ÜÜ	 

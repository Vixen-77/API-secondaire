//FIXME: ce code gère l'insertion de l'objet montre dans la base de donnée apres avoir cliqué sur le boutons connecte 
































































































/*Algorithm   1   :  Détection  Anomalie  (Patient_ID,  age,  FC_capte,  PAS_capte, 
PAD_capte,  TGS_capte)  par  un  smartphone  d’un  piéton
Begin
generated_req++ ;
if   (FC_capte  >  220  -  age)  or  (PAS_capte  >  130)  or  (PAS_capte  <  90)  or 
(PAD_capte  >  90)  or  (PAD_capte  <  60)  or  (TGS_capte  >  3)  or
(TGS_capte  <  0.5)  then
-  Activer  Stationnement  Automatique ; 
/*  Générer  Message  E merg ency  */
/*Emergency.Content  ←  ”Emergency” ;
Emergency.Request_ID  ←  Patient_ID  +  ”_”  +  simTime() ;
Emergency.Patient_ID  ←  Vi .Patient_ID ;
Emergency.Sender_Pos  ←  Vi .Position ;
Emergency.Edge_ID  ←  Vi .Edge_ID ;
Emergency.Request_State  ←  0;  /*  enAtt_enAtt  */ 
/*  Envoyer  Message  E merg ency  au  serveur  */ 
/*send(Emergency,Server) ;*/
/*end 
End*/
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////
/*Algorithm   2   :  Détection  Anomalie  (Patient_ID,  age,  FC_capte,  PAS_capte, 
PAD_capte,  TGS_capte)  par  une  OBU
Begin
if   (Vj .Type  ==  3)  then 
generated_req++ ;
if   (FC_capte  >  220  -  age)  or  (PAS_capte  >  130)  or  (PAS_capte  <  90)  or 
(PAD_capte  >  90)  or  (PAD_capte  <  60)  or  (TGS_capte  >  3)  or
(TGS_capte  <  0.5)  then
-  Activer  Stationnement  Automatique ; 
/*  Générer  Message  E merg ency  */
/*Emergency.Content  ←  ”Emergency” ;
Emergency.Request_ID  ←  Patient_ID  +  ”_”  +  simTime() ;
Emergency.Patient_ID  ←  Vi .Patient_ID ;
Emergency.Sender_Pos  ←  Vi .Position ;
Emergency.Edge_ID  ←  Vi .Edge_ID ;
Emergency.Request_State  ←  0;  /*  enAtt_enAtt  */ 
/*  Envoyer  Message  E merg ency  au  serveur  */ 
/*send(Emergency,Server) ;*/
/*  Générer  Message  Alert  */
/*Alert.Content  ←  ”Alert” ;
Alert.Request_ID  ←  Patient_ID  +  ”_”  +  simTime() ;
Alert.Patient_ID  ←  Vi .Patient_ID ;
Alert.Sender_Pos  ←  Vi .Position ;
Alert.Edge_ID  ←  Vi .Edge_ID ;
Alert.Request_State  ←  0;  /*  enAtt_enAtt  */
/*  Envoyer  Message  Alert  aux  voisins  chaque  2s  jusqu’à  la  fin  de  la 
recherche  */
/*send(Alert,Voisins) ; */
/*end 
End*/
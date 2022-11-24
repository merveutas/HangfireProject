# HangfireProject
.Net Core Mvc,NtierArchitecture,SqlServer,Nlog,ActionFilter,HangFire

Katmanlı mimari ile yazılan bu uygulamamda Hangfire sistemi kullanılarak arkaplanda çalışacak olan işler oluşturup, yürütmeyi ve yönetmeyi sağladım.

-> Kullanıcı sisteme kayıt olur. Ve hangfire servisi 1 kere tetiklenerek kullanıcıya kayıt işleminin başarılı olması durumunda mail gönderir.
->Kayıt işleminin ardından kullanıcı sisteme giriş yapabilir.
-> Kullanıcı, Sisteme uygulamalarının isimlerini ve urlleri kayıt edebilir. Listeleyebilir, güncelleyebilir.
-> Hangfire sistemi ile girilen url bilgilerine sürekli dünleme yapılır ve istek atılır. İstek statusu 200 değilse başarısız kabul edilir ve uygulama false olarak işaretlenir. (1. hangiFire servis)
-> Statusu false olan yani istek atılmış ancak url cevap vermemişse 2. HangFire servis dinleme yapar ve cevap vermeyen uygulamaların kullanıcılarına
otomatik olarak mail gönderilir. Mail servis, outlook olarak oluşturulmuştur ancak yapısı gereği değiştirilebilir durumdadır.



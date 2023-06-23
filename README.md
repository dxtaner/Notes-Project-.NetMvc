Notes Project
==============

Kurulum
-------

1.  \`HomeController\` sınıfını projenize ekleyin.
2.  Bağımlılıklarınızı doğru şekilde yönetmek için gerekli NuGet paketlerini yükleyin.
3.  \`NoteContext\` sınıfının veritabanı bağlantısını yapılandırın.

Özellikler
----------

*   `Index` metodu, veritabanındaki tüm notları alır ve bir görünüme ileterek kullanıcıya gösterir.
*   `Create` metodu, yeni bir not oluşturmak için HTTP GET isteğini işler ve oluşturma formunu kullanıcıya gösterir. Ayrıca tüm notları içeren bir `SelectList` nesnesini görünüme iletir.
*   `Create` metodu, yeni bir not oluşturmak için HTTP POST isteğini işler. Eğer gelen veriler geçerliyse, notu veritabanına ekler ve ilgili yönlendirmeyi yapar. Geçerli veriler yoksa, oluşturma formunu hatalarla birlikte tekrar gösterir.
*   `Delete` metodu, belirtilen bir notu silmek için HTTP POST isteğini işler. Silinecek notu veritabanından bulur, alt notlarıyla birlikte siler ve ilgili yönlendirmeyi yapar.
*   `MoveChildNotes` metodu, silinen bir notun altındaki notları üst notun altına taşır.

Kullanım
--------

1.  `Index` metodu, uygulamanın ana sayfasında tüm notları görüntülemek için kullanılır.
2.  `Create` metodu, yeni bir not oluşturmak için kullanılır. Bu metot, bir HTTP GET isteği ile çağrıldığında not oluşturma formunu gösterir ve bir HTTP POST isteği ile çağrıldığında yeni notu kaydeder.
3.  `Delete` metodu, belirtilen bir notu silmek için kullanılır. Silinecek notun kimliği `id` parametresi olarak alınır.

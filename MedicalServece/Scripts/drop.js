$(document).ready( function () {

    $(".tabs li").click(function(){
        var tab =  $(this).attr ('tab');
        $("#reports-pdf , #reports-img-X-x , #reports-img-analysis, #prescription-patients").hide();
        $("#" + tab).fadeIn()

    }); 
    
    
    uploadHBR.init({
                "target": "#uploads",
                "max": 200,
                "textNew": "ADD",
                "textTitle": "انقر هنا أو اسحب لتحميل صورة",
            });
            $('#reset').click(function () {
                uploadHBR.reset('#uploads');
            });
    
});


  $(window).load(function(){
              $('img').on('click', function(){
                  var src=$(this).attr('src');
                  $("#modal-img").attr('src', src);
                  $("#joes").modal('show');
              });
          });


function filePreview(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#uploadForm').after('<embed class="num" src="'+e.target.result+'" width="350" height="250">');
        };
        reader.readAsDataURL(input.files[0]);
    }
}
                
   $("#file").change(function () {
    filePreview(this);
});
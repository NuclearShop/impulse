var express    =       require("express");
var multer     =       require('multer');
var app        =       express();
var done       =       false;

app.use(multer({ dest: './uploads/',
 rename: function (fieldname, filename) {
    return filename+Date.now();
  },
onFileUploadStart: function (file) {
  console.log(file.originalname + ' is starting ...')
},
onFileUploadComplete: function (file) {
  console.log(file.fieldname + ' uploaded to  ' + file.path)
  done=true;
}
}));

app.post('/api/video',function(req,res){
  if(done==true){
    console.log(req.files);
    res.end("File uploaded.");
  }
});
app.post('/api/poster',function(req,res){
  if(done==true){
    console.log(req.files);
    res.end("File uploaded.");
  }
});

app.listen(9000,function(){
    console.log("Working on port 3000");
});

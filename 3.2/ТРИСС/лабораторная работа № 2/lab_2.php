<!DOCTYPE html>
<html>
<head>
<title>lab_2</title>
<meta charset="utf-8" />
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
<link rel="stylesheet" type="text/css" href="base.css">
</head>
    <body>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
       
        <div class="base">
        <?php
            if ($_FILES && $_FILES["filename"]["error"]== UPLOAD_ERR_OK)
            {
                $name = $_FILES["filename"]["name"];
                move_uploaded_file($_FILES["filename"]["tmp_name"], $name);

                $file = fopen($name, 'r') or die("не удалось открыть файл");
                $result = 0;

                echo "<h5>Результаты поиска</h5>  <br />";

                while(!feof($file))
                {
                    $str = htmlentities(fgets($file));
                    if (preg_match('/[А-Я]/u', $str)) {
                        echo $str."<br/>";
                        $result += 1;
                    }
                }
                echo "<br/>";

                if ($result == 0) echo "<p>Строки с прописными русскими символами не найдены</p> <br />";
                fclose($file);
            }
        ?>
            <h4>Загрузка файла</h4>
            <p>Выберите файл для считывания.</p>
            <form method="post" enctype="multipart/form-data" width="300px">
                <input class="form-control" type="file" name="filename" id="formFile"/><br />
                <input type="submit" value="Загрузить" class="btn btn-outline-success"/>
            </form>
        </div>
    </body>
</html>
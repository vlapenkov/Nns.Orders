﻿<h3>Краткое описание функционала</h3>
<ul>
<li>Реализован Rest Api</li>
<li>Применяемость видов работ и видов техники - много ко многим по выработке и дате</li>
<li>Сущность нельзя физически удалить, можно деактивировать (endDate)</li>
<li>Ввод задним числом документов не допускается</li>
<li>Все сущности иммутабельные, что позволяет формировать состояние как последовательность событий.    
Если применяемость или порядок работы нужно отменить, то это можно сделать только более поздней StartDate с тем же набором измерений.
Таким образом предотвращаем ввод системы в неконсистентное состояние.
В системе видно период действия каждого события и  избегаем возможного пересечения периодов [startDate,endDate).
</li>

</ul>
<ul>TODO:
<li>Error Middleware</li>
</ul>
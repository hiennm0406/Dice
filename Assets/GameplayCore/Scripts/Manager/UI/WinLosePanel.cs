using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLosePanel : MonoBehaviour
{
    /* thiết kế màn win lose như sau:
     * 
     * sau khi user thắng sẽ show màn win. Bên dưới có box show những vật phẩm nhận được trong trận, 
     * Ví dụ normal campaign sẽ nhận được vàng, totem trắng/xanh, mảnh vỡ totem (dùng để nâng cấp totem) 
     * và locked cheast, user sẽ xem video để mở toàn bộ các locked cheast.
     * Sau khi về Home, có anim tăng tiền và có chấm đỏ ở Inventory, những món đồ có thể nâng cấp đc sẽ có chấm đỏ
     * 
     * 
     * Nếu user thua. Màn hình hiện màn lose nhưng vẫn giữ lại hình ảnh God. Lần đầu tiên God sẽ diễn anim suy yếu.
     * Nút xem video để hồi sinh hiện ra. Bên dưới sẽ vẫn có box show vật phẩm có thể nhận được
     * kèm thông báo, you may lose anything
     * sau 3s thì hiện nút "từ bỏ" ở dưới
     * 
     * 
     * Nếu user ấn hồi sinh, màn hình lose biến mất, God diễn anim vùng dậy, đẩy tất cả địch về cuối màn hình, 
     * God sẽ tung xí ngầu ngay sau đấy
     * 
     * Nếu user từ bỏ. God gục ngã, các item phần thưởng sẽ diễn anim đốt cháy, 
     * những vật phẩm không phải god có tỷ lệ 70% bị đốt (mất)
     * gold thì có anim đốt nhưng sau đấy còn lại 30% giá trị.
     * Màn hình lose bị nứt vỡ và tự trở về Home sau 3s.
     * Khi về Home thì có anim tiền tăng lên.
     * 


    */





    public void ShowWin()
    {

    }

    public void ShowLose(bool canRetry)
    {

    }

    public void Retry()
    {

    }
}
